using System.Web.Mvc;

namespace SimpleBlog.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	public class UserController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}