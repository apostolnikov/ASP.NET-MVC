namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using Twitter.Web.Models;

    using Data;
    using Twitter.Data.UnitOfWork;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Twitter.Models;
    using System;

    public class TweetsController : BaseController
    {
        public TweetsController(ITwitterData ctx) : base(ctx)
        {
        }

        // GET: Tweet
        public ActionResult Index()
        {
            var allUsers = Data.Users.All().ToList();
            return this.View(allUsers);
        }

        [HttpPost]
        [Authorize]        
        public ActionResult FavorTweet(int id)
        {
            var userID = User.Identity.GetUserId();

            User loggedInUser = this.Data.Users.Find(userID);
            Tweet tweet = this.Data.Tweets.Find(id);
            
            Tweet favoredTweet = loggedInUser.MyFavoriteTweets.FirstOrDefault(t => t.Id == tweet.Id);

            if (favoredTweet == null)
            {
                loggedInUser.MyFavoriteTweets.Add(tweet);
                this.Data.SaveChanges();

                return Content("favored " + tweet.FavoredBy.Count());
            }
            else
            {
                loggedInUser.MyFavoriteTweets.Remove(tweet);
                this.Data.SaveChanges();

                return Content("favor " + tweet.FavoredBy.Count());
            }
        }
        
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string newTweetText)
        {
            if(ModelState.IsValid)
            {
                var userID = User.Identity.GetUserId();
                User loggedInUser = this.Data.Users.Find(userID);

                var newTweet1 = new Tweet()
                {
                    Text = newTweetText,
                    Author = loggedInUser,
                    CreatedAt = DateTime.Now
                };

                this.Data.Tweets.Add(newTweet1);
                this.Data.SaveChanges();
            }

            return Redirect("/");
        }
    }
}