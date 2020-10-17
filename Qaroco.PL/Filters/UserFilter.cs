using Qaroco.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qaroco.PL.Filters
{
	public class UserFilter : FilterAttribute, IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext filterContext)
		{

		}

		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Qaroco.DL.User user = null;
			user = (User)HttpContext.Current.Session["LoginUser"];
			if (user != null)
			{
				if (user.RoleId != 2)
				{
					filterContext.Result = new RedirectResult("/GirisYap");
				}
			}
		}
	}
}