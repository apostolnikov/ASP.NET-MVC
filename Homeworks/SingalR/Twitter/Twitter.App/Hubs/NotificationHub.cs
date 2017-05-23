using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Twitter.App.Hubs
{
    [HubName("notification")]
    public class NotificationHub : Hub
    {
        public void CountNotification(string userId, int notificationCount)
        {
            this.Clients.User(userId).countNotification(notificationCount);
        }
    }
}