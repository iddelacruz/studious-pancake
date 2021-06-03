namespace Application.MainBoundedContext.BatchModule
{
    using System;
    using System.Threading.Tasks;
    using Application.MainBoundedContext.DTO;
    using Domain.Seedwork.Contracts;
    using Domain.Seedwork.Events;

    public interface IBatchExecutor
    {
        event NotificationEventHandler Notify;
        Task RunAsync(BatchExecutorConfig configuration);
    }    
}
