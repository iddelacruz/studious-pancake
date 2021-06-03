
namespace Domain.Seedwork.Events
{
    using System;

    public class MetricEventArgs : NotificationEventArgs
    {
        public MetricEventArgs(string message) : base(message)
        {
        }
    }
}
