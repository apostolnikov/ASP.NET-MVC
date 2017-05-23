using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter.App.Models.ViewModels
{
    public class NotificationViewModel
    {
        public string Content { get; set; }
        public DateTime TimeOfNotification { get; set; }
    }
}