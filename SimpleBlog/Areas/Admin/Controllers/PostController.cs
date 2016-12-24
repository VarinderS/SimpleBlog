using System.Web.Mvc;

namespace SimpleBlog.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	public class PostController : Controller
	{
		public ActionResult Index()
		{
			return Content(content: "Post list");
		}
	}
}