

namespace Domain.MainBoundedContext.BatchModule.Services.Monitoring
{
    using System;
    using System.Collections.Generic;

    public sealed class Metric
    {
        public DateTime CollectionStarted { get; internal set; }

        public DateTime CollectionCompleted { get; internal set; }

        public TimeSpan TotalLatency { get; internal set; }

        public TimeSpan ListJobsLatency { get; internal set; }

        public Dictionary<string, JobMetrics> JobStats = new Dictionary<string, JobMetrics>();
        
        public Metric()
        {
        }
    }
}
