using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Twitter.App.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index(string username)
        {
            string a = username;
            return View(a);
        }
    }
}