using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SimpleBlog.Models;

namespace SimpleBlog.Controllers
{
	public abstract class BaseController : Controller
	{
		public SimpleBlogDbContext DbContext => HttpContext.GetOwinContext().Get<SimpleBlogDbContext>();	

		public ApplicationUserManager UserManager => HttpContext.GetOwinContext().Get<ApplicationUserManager>();

		public ApplicationSignInManager SignInManager => HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

		public ApplicationRoleManager RoleManager => HttpContext.GetOwinContext().Get<ApplicationRoleManager>();

		public IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
	}
}