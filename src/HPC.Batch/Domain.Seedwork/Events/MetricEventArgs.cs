
namespace Domain.Seedwork.Events
{
    public sealed class MetricEventArgs : NotificationEventArgs
    {
        public string JobID { get; private set; }

        public MetricEventArgs(string message) : base(message)
        {
        }
    }
}
