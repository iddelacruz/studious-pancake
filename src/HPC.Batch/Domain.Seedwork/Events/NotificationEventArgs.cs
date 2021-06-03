namespace Domain.Seedwork.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class NotificationEventArgs : EventArgs
    {
        public string Message { get; }
        protected NotificationEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
