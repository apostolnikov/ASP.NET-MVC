using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Twitter.App.Hubs
{
    [HubName("tweets")]
    public class TweetHub : Hub
    {
        public void SendMessage(string tweet)
        {
            this.Clients.All.receiveMessage(tweet);
        }
    }
}