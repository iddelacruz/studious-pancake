namespace Domain.Seedwork.Contracts
{
    using System;
    using Domain.Seedwork.Events;

    public interface INotificable
    {
        event EventHandler<MetricEventArgs> MetricsUpdated;
    }
}
