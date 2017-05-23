using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AjaxHw.Models;

namespace AjaxHw.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Search(string username)
        {
            var db = new ApplicationDbContext();
            var user = db.Users.FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                return this.Json(user, JsonRequestBehavior.AllowGet);
            }
            else return this.Json(1, JsonRequestBehavior.AllowGet);
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