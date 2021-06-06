namespace Domain.MainBoundedContext.BatchModule.Services.Monitoring
{
    using System;
    using System.Threading.Tasks;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs;

    internal class MetricCollector
    {
        private readonly IJobsRepository jobsRepositor;

        public MetricCollector(IJobsRepository jobsRepositor)
        {
            this.jobsRepositor = jobsRepositor;
        }

        public Task<Metric> CollectAsync()
        {
            throw new NotImplementedException();
        }
    }
}
