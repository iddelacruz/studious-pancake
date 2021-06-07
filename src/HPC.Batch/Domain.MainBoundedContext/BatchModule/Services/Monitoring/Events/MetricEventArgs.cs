
namespace Domain.MainBoundedContext.BatchModule.Services.Monitoring.Events
{
    using Domain.Seedwork.Events;

    public sealed class MetricEventArgs : NotificationEventArgs
    {
        /// <summary>
        /// Gets the job metrics at the last time the <see cref="MetricMonitor"/> updated them.
        /// </summary>
        public Metric Metric { get; private set; }

        public MetricEventArgs(Metric metric) : base("MetricEventArgs")
        {
            this.Metric = metric;
        }

        public MetricEventArgs(string message) : base(message)
        {
            
        }
    }
}
