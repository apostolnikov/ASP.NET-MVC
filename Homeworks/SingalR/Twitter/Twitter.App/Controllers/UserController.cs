using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Glimpse.Mvc.AlternateType;
using Microsoft.AspNet.Identity;
using Twitter.App.Models.ViewModels;
using Twitter.App.Models.ViewModels.HomeViewModels;
using Twitter.App.Models.ViewModels.UsersViewModels;
using Twitter.Models;

namespace Twitter.App.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        [Authorize]
        public ActionResult Index()
        {
            var userId = this.User.Identity.GetUserId();
            UserProfileViewModel model = new UserProfileViewModel();

            var user = this.Data.Users.GetById(userId);
            var followings = user.Followings;

            foreach (var following in followings)
            {
                var tweetsFromFollowing = following.OwnedTweets.OrderByDescending(t => t.TimeOfPublicated);

                foreach (var tweet in tweetsFromFollowing)
                {
                    model.Tweets.Add( new TweetViewModel()
                    {
                        Id = tweet.Id,
                        Content = tweet.Content,
                        TimeOfPublication = tweet.TimeOfPublicated,
                        Username = tweet.Owner.UserName
                    });
                }
            }

            model.Tweets.Sort((x,y) => DateTime.Compare(y.TimeOfPublication, x.TimeOfPublication));

            return View(model);
        }

        //GET
        [HttpGet]
        public ActionResult Profile(string id)
        {
            var user = this.Data.Users
                .Find(u => u.UserName == id)
                .FirstOrDefault(u => u.UserName == id);
            var notification = this.Data.Notifications
                .All()
                .Where(n => n.UserId == user.Id);

            UserProfileViewModel model = new UserProfileViewModel();

            model.Username = user.UserName;
            foreach (var tweet in user.OwnedTweets.OrderByDescending(t => t.TimeOfPublicated))
            {
                model.Tweets.Add(
                    new TweetViewModel()
                    {
                        Id = tweet.Id,
                        Username = tweet.Owner.UserName,
                        Content = tweet.Content,
                        TimeOfPublication = tweet.TimeOfPublicated
                    });
            }
            foreach (var not in notification)
            {
                model.Notifications.Add(new NotificationViewModel()
                {
                    Content = not.Content,
                    TimeOfNotification = not.TimeOfPublication
                });
            }

            model.FollowersCount = user.Followers.Count;
            model.FollowingsCount = user.Followings.Count;
            model.FavoritiesCount = user.FavoritesTweets.Count;

            return View(model);
        }       
    }
}