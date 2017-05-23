using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter.Data.UnitOfWork;

namespace Twitter.Web.Controllers
{
    public class CitiesController : BaseController
    {
        public CitiesController(ITwitterData ctx) : base(ctx)
        {
        }
        // GET: Cities
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetTowns(string start)
        {
            var towns = this.Data.Cities.All().Where(c => c.CityName.StartsWith(start));
            return Json(towns, JsonRequestBehavior.AllowGet);
        }
    }
}