namespace Twitter.Web.Controllers
{
    using Twitter.Web.Models;

    using System.Web.Mvc;
    using Twitter.Data.UnitOfWork;
    using Data;
    using System.Linq;
    using System.Collections.Generic;
    using PagedList;
    using Twitter.Models;
    using Microsoft.AspNet.Identity;
    using Models.ViewModels;
    using Customizations;
    using System;

    public class HomeController : BaseController
    {
        public HomeController(ITwitterData ctx) : base(ctx)
        {
        }

        [HttpGet]
        //[AuthorizedRedirectHome]
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var userID = User.Identity.GetUserId();

            if (userID != null)
            {
                return RedirectToRoutePermanent("MyHomePage");
            }

            IList<PagedTweetViewModel> tweets = this.Data.Tweets.All()
                                        .Select(PagedTweetViewModel.Create)
                                        .OrderByDescending(t => t.CreatedAt).ToList();

            PagedList<PagedTweetViewModel> pagedTweets = new PagedList<PagedTweetViewModel>(tweets, page, pageSize);

            List<string> usernameList = this.Data.Users.All().Where(u => !u.Roles.Any())
                .Select(u => u.UserName).OrderBy(u => u).ToList();

            var model = new Tuple<PagedList<PagedTweetViewModel>, List<string>>(pagedTweets, usernameList);

            return View(model);                      
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