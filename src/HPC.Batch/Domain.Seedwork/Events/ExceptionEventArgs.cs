namespace Domain.Seedwork.Events
{
    using System;

    public sealed class ExceptionEventArgs : NotificationEventArgs
    {
        public ExceptionEventArgs(Exception ex) : base(ex.Message)
        {
        }
    }
}
