using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qaroco.PL.Filters
{
    public class AuthorizeFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = HttpContext.Current.Session["LoginUser"];
            if (user == null)
            {
                filterContext.Result = new RedirectResult("/Error/PageError");
            }
        }
    }
}