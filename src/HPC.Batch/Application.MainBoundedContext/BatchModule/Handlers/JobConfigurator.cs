
namespace Application.MainBoundedContext.BatchModule.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.MainBoundedContext.DTO;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs.Builders;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs.Decorators;
    using Domain.MainBoundedContext.BatchModule.Aggregates.NodePools;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Tasks.Builders;

    internal class JobConfigurator : Configurator
    {

        public JobConfigurator(BatchExecutorConfig config) : base(config)
        {
            
        }

        public override async Task<INodePool> ConfigureAsync(INodePool request)
        {
            if (this.config.Resources is null || !this.config.Resources.Any())
            {
                throw new InvalidOperationException("Resource object can't be null or empty.");
            }

            if (this.config.JobsConfig is null)
            {
                throw new ArgumentNullException(nameof(this.config.JobsConfig));
            }

            if (this.config.TasksConfig is null)
            {
                throw new ArgumentNullException(nameof(this.config.TasksConfig));
            }

            var jobConfig = this.config.JobsConfig;

            foreach (var resources in ChunkInternal(this.config.Resources, jobConfig.TasksPerJob))
            {
                var job = GiveMeTheJob(jobConfig);

                foreach (var resource in resources)
                {
                    job.Tasks.Add(GiveMeTheTask(this.config.TasksConfig, resource));
                }

                request.Jobs.Add(job);
            }

            return await base.ConfigureAsync(request);
        }

        private static IEnumerable<IEnumerable<ResourceFile>> ChunkInternal(IEnumerable<ResourceFile> source, uint chunkSize)
        {
            if (chunkSize == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(chunkSize), "The chunkSize parameter must be a greather than 0.");
            }

            using IEnumerator<ResourceFile> enumerator = source.GetEnumerator();
            do
            {   
                if (!enumerator.MoveNext())
                {
                    yield break;
                }

                yield return ChunkSequence(enumerator, chunkSize);
            }
            while (true);
        }

        private static IEnumerable<ResourceFile> ChunkSequence(IEnumerator<ResourceFile> enumerator, uint chunkSize)
        {
            int count = 0;

            do
            {
                yield return enumerator.Current;
            }
            while (++count < chunkSize && enumerator.MoveNext());
        }

        private static Job GiveMeTheJob(JobsConfig config)
        {
            var jobId = config.Prefix + Guid.NewGuid();

            var job = new JobBuilder()
                .ID(jobId)
                .Contraints(config.MaxTaskRetryCount ?? 0, config.MaxWallClockTime)
                .TaskFailureAction((TaskFailure)(config.TaskFailedAction ?? 0))//Test the result when the value is greather than 1
                .Build();          

            if(config.PreparationTask is not null)
            {
        
                job = new AddJobPreparationTask(job,
                    new PreparationTaskBuilder()
                    .ID($"preparation_task_{jobId}")
                    .Command(config.PreparationTask.Command)
                    .ResourceFile(config.PreparationTask.ResourceFile?.ContainerName, config.PreparationTask.ResourceFile?.BlobName)
                    .TaskConstraints(
                        config.PreparationTask.MaxTaskRetryCount ?? 0,
                        config.PreparationTask.MaxWallClockTime,
                        config.PreparationTask.RetentionTime)
                    .WaitForSuccess(config.PreparationTask.WaitForSuccess ?? false)
                    .ReRunOnComputeNodeRebootAfterSuccess(
                        config.PreparationTask.ReRunOnComputeNodeRebootAfterSuccess ?? false)
                    .Build());
            }

            if(config.ReleaseTask is not null)
            {
                new AddJobReleaseTask(job, new ReleaseTaskBuilder()
                    .ID($"release_task_{jobId}")
                    .Command(config.ReleaseTask.Command)
                    .ResourceFile(config.ReleaseTask.ResourceFile?.ContainerName, config.ReleaseTask.ResourceFile?.BlobName)
                    .TaskConstraints(config.ReleaseTask.MaxWallClockTime, config.ReleaseTask.RetentionTime)
                    .Build());
            }

            return job;
        }

        private static ITask GiveMeTheTask(TaskConfig config, ResourceFile resource)
        {
            if (string.IsNullOrWhiteSpace(config.Prefix))
            {
                throw new ArgumentNullException(nameof(config.Prefix));
            }

            var taskId = config.Prefix + Guid.NewGuid();
            //TODO: Create task

            var builder = new TaskBuilder()
                .ID(taskId)
                .Command(config.Command)
                .TaskConstraints(config.MaxTaskRetryCount ?? 0, config.MaxWallClockTime, config.RetentionTime)
                .ResourceFile(resource.ContainerName, resource.BlobName);

            return builder.Build();    
        }
    }
}
