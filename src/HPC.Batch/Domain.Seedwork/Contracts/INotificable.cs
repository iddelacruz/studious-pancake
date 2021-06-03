﻿namespace Domain.Seedwork.Contracts
{
    using System;
    using Domain.Seedwork.Events;

    public interface INotificable
    {
        event NotificationEventHandler Notify;
    }

    public delegate void NotificationEventHandler(object sender, NotificationEventArgs e);
}
