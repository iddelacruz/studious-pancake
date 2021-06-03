namespace Infrastructure.Data.MainBoundedContext.BatchModule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs.Builders;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs.Decorators;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Builders;
    using Domain.Seedwork.Contracts;
    using Domain.Seedwork.Events;
    using Infrastructure.Data.Seedwork;
    using Jobs;
    using Microsoft.Azure.Batch;
    using Microsoft.Azure.Batch.Common;
    using Monitoring;
    using Tasks;

    public sealed class AzureBatchJobsRepository : IJobsRepository, IDisposable
    {
        public event NotificationEventHandler Notify;

        private readonly BatchClient client;

        private readonly ICollection<CloudJob> cloudJobs = new HashSet<CloudJob>();

        /// <summary>
        /// Create a new instance of <see cref="AzureBatchJobsRepository"/>
        /// </summary>
        /// <param name="owner">The <see cref="INodePool"> to which it belongs</param>
        public AzureBatchJobsRepository(IBatchClient<BatchClient> client)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            this.client = client.GiveMeTheClient() ?? throw new ArgumentNullException(nameof(this.client));
        }

        /// <summary>
        /// Add a <see cref="Job"/> to the <see cref="INodePool"/>
        /// </summary>
        /// <param name="entity">The <see cref="Job"/> to be added.</param>
        public async Task AddAsync(Job entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                //move this.
                using var monitor = new MetricMonitor(this.client);
                monitor.MetricsUpdated += OnMetricsUpdated;
                monitor.Start();

                var cloudJob = await this.GetIfExistAsync(entity.Identifier);

                if (cloudJob is null)
                {
                    cloudJob = new CloudJobCreator(this.client, entity).Create();

                    await cloudJob.CommitAsync();

                    this.cloudJobs.Add(cloudJob);
                }

                var cloudTasks = new List<CloudTask>(entity.Tasks.Count);

                foreach (var task in entity.Tasks)
                {
                    cloudTasks.Add((AzureCloudTask)task);
                }

                //TODO: it is recomendable to send 100 task at time
                await this.client.JobOperations.AddTaskAsync(entity.Identifier, cloudTasks);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Fin all the <see cref="Job"/> assocoated to a <see cref="INodePool"/>
        /// </summary>
        /// <returns>a collection of <see cref="Job"/></returns>
        public Task<IEnumerable<Job>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find an element by identifier
        /// </summary>
        /// <param name="identifier"><see cref="Job"/> unique identifier.</param>
        /// <returns>A <see cref="Job"/> if exist or null if not.</returns>
        public async Task<Job> GetAsync(string identifier)
        {
            Job job = null;

            CloudJob cloudJob = await this.GetIfExistAsync(identifier);
            
            if (cloudJob is not null)
            {
                var builder = new JobBuilder();
                builder.ID(cloudJob.Id);

                if (cloudJob.Priority.HasValue)
                {
                    builder.Priority((short)cloudJob.Priority.Value);
                }

                if (cloudJob.OnTaskFailure.HasValue)
                {
                    builder.TaskFailureAction(cloudJob.OnTaskFailure.Value == OnTaskFailure.NoAction
                    ? TaskFailure.NoAction
                    : TaskFailure.PerformExitOptionsJobAction);
                }    

                if(cloudJob.Constraints is not null)
                {
                    DateTime? date = null;
                    if (cloudJob.Constraints.MaxWallClockTime.HasValue)
                    {
                        //TODO: revisar
                        date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                            .AddSeconds(cloudJob.Constraints.MaxWallClockTime.Value.TotalMilliseconds)
                            .ToLocalTime();
                    }      
                    builder.Contraints(cloudJob.Constraints.MaxTaskRetryCount ?? 0, date);
                }

                job = builder.Build();
                if(cloudJob.JobPreparationTask is not null)
                {
                    new AddJobPreparationTask(
                        job,
                        new PreparationTaskBuilder()
                        .ID(cloudJob.JobPreparationTask.Id)
                        .Command(cloudJob.JobPreparationTask.CommandLine)
                        .Build())
                        .Apply();
                }

                if (cloudJob.JobPreparationTask is not null)
                {
                    new AddJobReleaseTask(
                        job,
                        new ReleaseTaskBuilder()
                        .ID(cloudJob.JobReleaseTask.Id)
                        .Command(cloudJob.JobReleaseTask.CommandLine)
                        .Build())
                        .Apply();
                }
            }
            return job;
        }

        /// <summary>
        /// Delete a specific job.
        /// </summary>
        /// <param name="identifier"><see cref="Job"/> unique identifier.</param>
        public Task<bool> RemoveAsync(string identifier)
        {
            //TODO: Complete
            throw new NotImplementedException();
        }

        private async Task<CloudJob> GetIfExistAsync(string jobId)
        {
            try
            {
                var job = this.cloudJobs.FirstOrDefault(x => x.Id.Equals(jobId));

                if (job == null)
                {
                    // Construct a detail level with a filter clause that specifies the job ID so that only
                    // a single CloudJob is returned by the Batch service (if that job exists)
                    var detail = new ODATADetailLevel(filterClause: string.Format("id eq '{0}'", jobId));
                    List<CloudJob> jobs = await this.client.JobOperations.ListJobs(detailLevel: detail)
                        .ToListAsync()
                        .ConfigureAwait(continueOnCapturedContext: false);

                    job = jobs.FirstOrDefault();

                    if (job != null)
                    {
                        cloudJobs.Add(job);
                    }
                }
                return job;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void OnMetricsUpdated(object sender, EventArgs e)
        {
            //TODO: fill this object
            this.Notify?.Invoke(this, new MetricEventArgs(""));
        }

        #region disposable
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.client != null)
                {
                    this.client.Dispose();
                }

                //TODO: delete event susciption
            }            
        }
        #endregion
    }
}
