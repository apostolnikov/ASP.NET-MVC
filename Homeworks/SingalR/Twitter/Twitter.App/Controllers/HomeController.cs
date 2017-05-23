using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Twitter.App.Hubs;
using Twitter.App.Models.ViewModels.HomeViewModels;
using Twitter.Models;
using WebGrease.Css.Extensions;

namespace Twitter.App.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var userId = this.User.Identity.GetUserId();

            if (userId != null)
            {
                return RedirectToAction("Index", "User");
            }
            var tweets = this.Data.Tweets.All()
                .OrderByDescending(t => t.TimeOfPublicated)
                .Take(10);
            List<TweetViewModel> tweetsViewModel = new List<TweetViewModel>();
            foreach (var tweet in tweets)
            {
                TweetViewModel tweetView = new TweetViewModel();
                tweetView.Content = tweet.Content;
                tweetView.TimeOfPublication = tweet.TimeOfPublicated;
                tweetView.Username = this.Data.Users.GetById(tweet.OwnerId).UserName;
                tweetsViewModel.Add(tweetView);
            }
            return View(tweetsViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}