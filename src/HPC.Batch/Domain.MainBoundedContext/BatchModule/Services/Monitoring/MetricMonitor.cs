
namespace Domain.MainBoundedContext.BatchModule.Services.Monitoring
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.MainBoundedContext.BatchModule.Aggregates.Jobs;
    using Domain.Seedwork.Contracts;
    using Domain.Seedwork.Events;
    using Events;

    public sealed class MetricMonitor : IMonitorService
    {

        public event NotificationEventHandler Notify;

        private readonly MetricCollector collector;

        private readonly object locker = new();

        private readonly TimeSpan monitorInterval;

        private readonly CancellationTokenSource runCancel = new();

        private Task runTask;

        public MetricMonitor(IJobsRepository jobsRepository)
        {
            collector = new MetricCollector(jobsRepository ?? throw new ArgumentNullException(nameof(jobsRepository)));
        }

        /// <summary>
        /// Starts monitoring the Azure Batch account and gathering metrics.  Once Start has been called,
        /// metrics will be updated periodically: the latest metrics can be found in <see cref="Metric"/>,
        /// and the <see cref="Notify"/> event is raised on each update.
        /// </summary>
        public void Start()
        {
            lock (this.locker)
            {
                if (this.runTask is null)
                {
                    this.runTask = Task.Run(() => Run());
                }
            }
        }

        // The main monitoring engine. This method runs continuously until the monitor is disposed.
        // Each time round the loop it calls the Batch service to get task metrics,
        // then waits for the monitoring interval before going round the loop again.
        private async Task Run()
        {
            while (!this.runCancel.IsCancellationRequested)
            {      
                OnNotify(new MetricEventArgs(await this.collector.CollectAsync()));

                try
                {
                    await Task.Delay(this.monitorInterval, this.runCancel.Token);
                }
                catch (TaskCanceledException)
                {
                    throw;
                }
            }
        }

        private void OnNotify(NotificationEventArgs e)
        {
            Notify?.Invoke(this, e);
        }
    }
}
