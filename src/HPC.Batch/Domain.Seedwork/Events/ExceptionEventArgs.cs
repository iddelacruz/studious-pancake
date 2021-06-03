namespace Domain.Seedwork.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExceptionEventArgs : NotificationEventArgs
    {
        public ExceptionEventArgs(string message) : base(message)
        {
        }
    }
}
