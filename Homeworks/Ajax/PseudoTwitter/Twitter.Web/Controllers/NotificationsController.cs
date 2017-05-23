namespace Twitter.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using System.Web.Mvc;
    using Twitter.Data;
    using Twitter.Data.UnitOfWork;
    using System;
    using System.Linq;
    using Twitter.Models;
    using Models.ViewModels;
    using PagedList;

    public class NotificationsController : BaseController
    {
        //public NotificationsController() : base(new TwitterData(new TwitterContext()))
        //{
        //}

        public NotificationsController(ITwitterData ctx) : base(ctx)
        {
        }

        // GET: Notifications
        [Authorize]
        [Route(Name = "NotificationsRoute")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]        
        public ActionResult ReturnAllNotifications()
        {
            var userID = User.Identity.GetUserId();
            var user = this.Data.Users.Find(userID);

            var notifications = user.NotificationsDelivered.AsQueryable().Select(PagedNotificationViewModel.Create);

            PagedList<PagedNotificationViewModel> pagedNotifications = new PagedList<PagedNotificationViewModel>(notifications, 1, 10);

            return PartialView("_PagedNotifications", pagedNotifications);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ReturnFollowedUsersNotifications()
        {
            var userID = User.Identity.GetUserId();
            var user = this.Data.Users.Find(userID);

            var notifications = user.NotificationsDelivered.Where(n => user.Following.Any(u => u.Id == n.TriggeredBy.Id)).AsQueryable().Select(PagedNotificationViewModel.Create);

            PagedList<PagedNotificationViewModel> pagedNotifications = new PagedList<PagedNotificationViewModel>(notifications, 1, 10);

            return PartialView("_PagedNotifications", pagedNotifications);
        }
    }
}