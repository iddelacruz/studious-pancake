namespace Domain.MainBoundedContext.BatchModule.Services.Monitoring
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs;

    internal class MetricCollector
    {
        private readonly IJobsRepository jobsRepository;
        private readonly Dictionary<string, TaskStateCache> jobStateCache = new Dictionary<string, TaskStateCache>();

        public MetricCollector(IJobsRepository jobsRepository)
        {
            this.jobsRepository = jobsRepository;
        }

        public async Task<Metric> CollectAsync()
        {
            try
            {
                var totalLatency = Stopwatch.StartNew();
                var listJobsTimer = Stopwatch.StartNew();

                var jobs = await this.jobsRepository.GetAllAsync();

                listJobsTimer.Stop();

                var metric = new Metric
                {
                    ListJobsLatency = listJobsTimer.Elapsed
                };

                foreach (var job in jobs)
                {
                    await CollectTaskMetricsAsync(metric, job);
                }

                totalLatency.Stop();
                metric.TotalLatency = totalLatency.Elapsed;
                metric.CollectionCompleted = DateTime.UtcNow;

                return metric;

            }
            catch (Exception)
            {
                throw;
            }
        }

        // Calls the Batch service to get metrics for a single job.  The first time the
        // MetricMonitor sees a job, it creates a TaskStateCache to hold task state information,
        // and queries the states of *all* tasks in the job. Subsequent times, it queries
        // only for tasks whose states have changed since the previous query -- this significant
        // reduces download volumes for large jobs. In either case, it then updates the
        // cached task states and aggregates them into a TaskStateCounts object.
        private async Task CollectTaskMetricsAsync(Metric metric, Job job)
        {
            TaskStateCache taskStateCache;

            bool firstTime = !this.jobStateCache.ContainsKey(job.Identifier);

            if (firstTime)
            {
                taskStateCache = new TaskStateCache();
                this.jobStateCache.Add(job.Identifier, taskStateCache);
            }
            else
            {
                taskStateCache = this.jobStateCache[job.Identifier];
            }

            // If the monitor API is called for the first time, it has to issue a query to enumerate all the tasks once to get its state.
            // This is a relatively slow query.
            // Subsequent calls to the monitor API will only look for changes to the task state since the last time the query was issued and 
            // a clock skew (which is within 30 seconds approximately for Azure). Thus if the monitoring API periodicity is 1 minute, then the query 
            // should look for changes in the last minute and 30 seconds.

            // TODO: it would be better to record the time at which the last query was issued and use that,
            // rather than subtracting the monitor interval from the current time
            DateTime since = DateTime.UtcNow - (this.monitorInterval + MaximumClockSkew);
            var tasksToList = firstTime ? DetailLevels.IdAndState.AllEntities : DetailLevels.IdAndState.OnlyChangedAfter(since);

            var listTasksTimer = Stopwatch.StartNew();
            var tasks = await job.ListTasks(tasksToList).ToListAsync(this.runCancel.Token);
            listTasksTimer.Stop();

            var listTasksLatency = listTasksTimer.Elapsed;

            foreach (var task in tasks)
            {
                taskStateCache.UpdateTaskState(task.Id, task.State.Value);
            }

            var taskStateCounts = taskStateCache.GetTaskStateCounts();

            metric.JobStats.Add(job.Identifier, new JobMetrics(listTasksLatency, taskStateCounts));
        }
    }
}
