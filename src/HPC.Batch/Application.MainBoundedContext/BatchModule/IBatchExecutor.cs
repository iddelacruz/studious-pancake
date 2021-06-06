namespace Application.MainBoundedContext.BatchModule
{
    using System;
    using System.Threading.Tasks;
    using Application.MainBoundedContext.DTO;
    using Domain.Seedwork.Contracts;

    public interface IBatchExecutor : INotificable, IDisposable
    {
        Task RunAsync(BatchExecutorConfig configuration);
    }    
}
