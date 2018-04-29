using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DrugStore.Helpers
{
    public static class CustomRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string AdminOrUser = Admin + "," + User;
    }

    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //if not logged, it will work as normal Authorize and redirect to the Login
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Login" }));
            }
            else
            {
                //logged and wihout the role to access it - redirect to the custom controller action
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
            }
        }
    }
}