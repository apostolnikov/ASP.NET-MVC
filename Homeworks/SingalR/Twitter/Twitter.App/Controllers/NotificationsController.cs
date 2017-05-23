using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Twitter.App.Hubs;
using Twitter.App.Models.ViewModels;
using Twitter.Models;
using WebGrease.Configuration;

namespace Twitter.App.Controllers
{
    [System.Web.Mvc.Authorize]
    public class NotificationsController : BaseController
    {
        public ActionResult Favorite(int id)
        {
            var tweet = this.Data.Tweets.GetById(id);
            var user = this.Data.Users.GetById(this.User.Identity.GetUserId());

            Notification not = new Notification();
            string content = string.Format("User {0} add this tweet: \"{1}\" to his favorites tweets", user.UserName, tweet.Content);
            not.UserId = tweet.OwnerId;
            not.Content = content;

            user.FavoritesTweets.Add(tweet);

            this.Data.Notifications.Add(not);

            this.Data.SaveChanges();

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            int notificationCount = this.Data.Notifications.All().Where(n => n.UserId == not.UserId).Count();
            hubContext.Clients.User(not.UserId).countNotification(notificationCount);

            return RedirectToAction("Index", "User");
        }

        public ActionResult Retweet()
        {
            // TO DO
            return View("Hello");
        }

        [HttpPost]
        public ActionResult BecomeFollowing(string id)
        {
            var curentUser = this.Data.Users.GetById(this.User.Identity.GetUserId());
            var follingUser = this.Data.Users
                .Find(u => u.UserName == id)
                .FirstOrDefault(u => u.UserName == id);

            curentUser.Followings.Add(follingUser);
            follingUser.Followers.Add(curentUser);

            Notification not = new Notification()
            {
                UserId = follingUser.Id,
                Content = "User " + curentUser.UserName + " follower you!"
            };

            this.Data.Notifications.Add(not);

            this.Data.SaveChanges();

            return RedirectToAction("Index", "User");
        }

        public ActionResult SeeNotification()
        {
            var userId = this.User.Identity.GetUserId();
            var notification = this.Data.Notifications
                .All()
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.TimeOfPublication)
                .ToList();

            List<NotificationViewModel> model = new List<NotificationViewModel>();

            foreach (var not in notification)
            {
                model.Add(new NotificationViewModel()
                {
                    Content = not.Content,
                    TimeOfNotification = not.TimeOfPublication
                });
            }
            return View(model);
        }
    }
}