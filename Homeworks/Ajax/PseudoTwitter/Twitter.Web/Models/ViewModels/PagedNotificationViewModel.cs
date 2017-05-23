using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web;
using Twitter.Models;
using Twitter.Models.Enums;

namespace Twitter.Web.Models.ViewModels
{
    public class PagedNotificationViewModel
    {
        public static Expression<Func<Notification, PagedNotificationViewModel>> Create
        {
            get
            {
                return n => new PagedNotificationViewModel
                {
                    Id = n.Id,
                    Content = n.Content,
                    Date = n.Date,
                    SendTo = n.SendTo.Id,
                    TriggeredBy = n.TriggeredBy.Id,
                    Read = n.Read
                };
            }
        }

        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Content { get; set; }

        [Required(ErrorMessage ="Send date is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage ="Notification Recipient is required.")]
        public string SendTo { get; set; }

        [Required(ErrorMessage = "Notification Sender is required.")]
        public string TriggeredBy { get; set; }

        [EnumDataType(typeof(NotificationKind))]
        public NotificationKind NotificationKind { get; set; }

        public bool Read { get; set; }
    }
}