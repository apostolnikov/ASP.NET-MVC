using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter.Data;
using Twitter.Data.UnitOfWork;

namespace Twitter.App.Controllers
{
    public class BaseController : Controller
    {
        protected ITwitterData Data { get; private set; }

        public BaseController(ITwitterData data)
        {
            this.Data = data;
        }
        public BaseController()
            :this(new TwitterData(new TwitterDb()))
        {
            
        }

    }
}