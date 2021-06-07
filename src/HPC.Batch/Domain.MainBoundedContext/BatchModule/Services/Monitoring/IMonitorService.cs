namespace Domain.MainBoundedContext.BatchModule.Services.Monitoring
{
    using Domain.Seedwork.Contracts;

    public interface IMonitorService : INotificable
    {
        void Start();
        void Stop();
    }
}
