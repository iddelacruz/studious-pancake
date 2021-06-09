namespace Domain.MainBoundedContext.BatchModule.Services.Monitoring
{
    using System;
    using Domain.Seedwork.Contracts;

    public interface IMonitorService : INotificable, IDisposable
    {
        void Start();
        void Stop();
    }
}
