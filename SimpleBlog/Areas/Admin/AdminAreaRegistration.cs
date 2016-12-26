﻿using SimpleBlog.Areas.Admin.Controllers;
using System.Web.Mvc;

namespace SimpleBlog.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
			var nameSpaces = new string[] { typeof(PostController).Namespace };

			context.MapRoute(
                "Admin_default",
                "admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
				nameSpaces
			);
        }
    }
}