namespace Domain.Seedwork.Events
{
    using System;

    public class ExceptionEventArgs : NotificationEventArgs
    {
        public ExceptionEventArgs(Exception ex) : base(ex.Message)
        {
        }
    }
}
