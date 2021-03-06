﻿namespace Twitter.Web.Customizations
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class AuthorizeOrRedirectHome : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary("Default"));
            }
        }
    }
}