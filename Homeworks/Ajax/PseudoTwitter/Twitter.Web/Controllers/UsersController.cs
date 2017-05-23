namespace Twitter.Web.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using Twitter.Data.UnitOfWork;
    using Models;
    using PagedList;
    using Customizations;
    using Microsoft.AspNet.Identity;
    using Models.ViewModels;
    using System.Collections.Generic;
    using Twitter.Models;
    using AutoMapper;
    using System.Web.Routing;
    using System;

    public class UsersController : BaseController
    {
        public UsersController(ITwitterData ctx) : base(ctx)
        {
        }

        [HttpGet]
        //[AuthorizeOrRedirectHome]
        [Route(Name = "MyHomePage")]
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();

            if (userID == null)
            {
                return RedirectToRoutePermanent("Default");
            }

            var user = this.Data.Users.Find(userID);

            var userModel = Mapper.Map<User, UserViewModel>(user);
            
            userModel.IsMe = true;

            return View("MyHomePage", userModel);
        }

        [HttpGet]
        [Route(Name = "UserHomePage")]
        public ActionResult ShowUserHomePage(string username)
        {
            var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

            if (user != null)
            {
                var userModel = Mapper.Map<User, UserViewModel>(user);

                var loggedInUserId = User.Identity.GetUserId();

                if (loggedInUserId != null)
                {
                    TempData["loggedInUser"] = true;
                    if (loggedInUserId == user.Id)
                        userModel.IsMe = true;
                }
                else
                    TempData["loggedInUser"] = false;

                userModel.UserProfile = true;

                return View("ProfilePage", userModel);
            }

            return Redirect("Home/Index");
        }

        
        [HttpGet]
        [Route("Users/Redirect")]
        public ActionResult Redirect([FromUrl]string username, [FromUrl]string load)
        {
            TempData["load"] = load;

            return RedirectToAction("ShowUserHomePage", new { username = username });
        }

        [HttpPost]
        public ActionResult ReturnTweets(string userId, int pageStart = 1, int pageSize = 10)
        {
            var tweets = this.Data.Tweets.All().Where(t => t.Author.Id == userId || t.Author.Following.Any(f => f.Id == userId))
                .Select(PagedTweetViewModel.Create).OrderByDescending(t => t.CreatedAt).ToList();

            PagedList<PagedTweetViewModel> pagedTweets = new PagedList<PagedTweetViewModel>(tweets, pageStart, pageSize);

            return PartialView("_PagedTweets", pagedTweets);
        }

        [HttpPost]
        public ActionResult ReturnOwnTweets(string userId, int pageStart = 1, int pageSize = 10)
        {
            var tweets = this.Data.Tweets.All().Where(t => t.Author.Id == userId)
                .Select(PagedTweetViewModel.Create).OrderByDescending(t => t.CreatedAt).ToList();

            PagedList<PagedTweetViewModel> pagedTweets = new PagedList<PagedTweetViewModel>(tweets, pageStart, pageSize);

            return PartialView("_PagedTweets", pagedTweets);
        }

        [HttpPost]
        public ActionResult ReturnFavoriteTweets(string userId, int pageStart = 1, int pageSize = 10)
        {
            var tweets = this.Data.Users.Find(userId).MyFavoriteTweets.AsQueryable()
                .Select(PagedTweetViewModel.Create).OrderByDescending(t => t.CreatedAt).ToList();

            PagedList<PagedTweetViewModel> pagedTweets = new PagedList<PagedTweetViewModel>(tweets, pageStart, pageSize);

            return PartialView("_PagedTweets", pagedTweets);
        }

        [HttpPost]
        public ActionResult ReturnFollowers(string userId, int pageStart = 1, int pageSize = 10)
        {
            var users = this.Data.Users.Find(userId).Followers.AsQueryable()
                .Select(PagedUserViewModel.Create).OrderByDescending(t => t.Username).ToList();

            PagedList<PagedUserViewModel> pagedUsers = new PagedList<PagedUserViewModel>(users, pageStart, pageSize);

            return PartialView("_PagedUsers", pagedUsers);
        }

        [HttpPost]
        public ActionResult ReturnFollowing(string userId, int pageStart = 1, int pageSize = 10)
        {
            var users = this.Data.Users.Find(userId).Following.AsQueryable()
                .Select(PagedUserViewModel.Create).OrderByDescending(t => t.Username).ToList();

            PagedList<PagedUserViewModel> pagedUsers = new PagedList<PagedUserViewModel>(users, pageStart, pageSize);

            return PartialView("_PagedUsers", pagedUsers);
        }

        [HttpGet]
        public ActionResult CheckUsername(string username)
        {
            string replay = null;

            if (username.Length > 0 && username.Length < 5)
            {
                replay = "A valid username must have at least 5 letters";
            } else if (username.Length >= 4)
            {
                var users = this.Data.Users.All().Where(u => u.UserName == username).FirstOrDefault();

                if (users == null)
                    replay = "Username \"" + username + "\" is free";
                else
                    replay = "Username \"" + username + "\" is taken";
            }        

            return Content(replay);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ToolTip(string username)
        {
            var uName = username.Substring(1);
            var user = this.Data.Users.All().Where(u => u.UserName == uName).FirstOrDefault();
            var toolTip = Mapper.Map<User, ToolTipUserModel>(user);

            return PartialView("_ToolTipUserViewModel", toolTip);
        }

        private class FromUrlAttribute : Attribute
        {
        }
    }
}