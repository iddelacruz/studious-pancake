namespace Application.MainBoundedContext.BatchModule
{
    using System;
    using System.Threading.Tasks;
    using Application.MainBoundedContext.DTO;
    using Domain.Seedwork.Events;

    public interface IBatchExecutor
    {
        event EventHandler<MetricEventArgs> MetricsUpdated;
        Task RunAsync(BatchExecutorConfig configuration);
    }    
}
