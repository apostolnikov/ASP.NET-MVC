using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Twitter.App.Hubs;
using Twitter.App.Models.BindingModels;
using Twitter.App.Models.ViewModels.HomeViewModels;
using Twitter.Models;

namespace Twitter.App.Controllers
{
    public class TweetController : BaseController
    {

        //POST
        [System.Web.Mvc.Authorize]
        [HttpPost]
        public ActionResult CreateTweet(CreateTweetBindingModel model)
        {
            
            if (ModelState.IsValid && model.TweetContent != null)
            {
                Tweet tweet = new Tweet();
                tweet.OwnerId = this.User.Identity.GetUserId();
                tweet.Content = model.TweetContent;
                this.Data.Tweets.Add(tweet);
                this.Data.Users.GetById(this.User.Identity.GetUserId()).OwnedTweets.Add(tweet);

                this.Data.SaveChanges();
                TweetViewModel tweetModel = new TweetViewModel()
                {
                    Id = tweet.Id,
                    Content = model.TweetContent,
                    TimeOfPublication = tweet.TimeOfPublicated,
                    Username = this.User.Identity.GetUserName()
                };

                var hubContext = GlobalHost.ConnectionManager.GetHubContext<TweetHub>();
                hubContext.Clients.All.receiveMessage(tweetModel);

                return RedirectToAction("Index", "User");
            }
            else
            {
                throw new ArgumentOutOfRangeException("The content of tweet must be less than 300 symbols!");
            }

            return RedirectToAction("Index", "User");
        }
    }
}