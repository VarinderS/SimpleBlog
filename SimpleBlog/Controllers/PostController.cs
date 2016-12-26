using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using SimpleBlog.ViewModels;

namespace SimpleBlog.Controllers
{
	public class PostController : BaseController
	{
		public ActionResult Details(int id, string slug)
		{
			if (id == 0)
			{
				return new HttpNotFoundResult(statusDescription: "Post not found");
			}

			var selectedPost = DbContext.Posts
									.OrderBy(keySelector: post => post.Title)
									.Where(predicate: post => post.DateDeleted == null)
									.Include(path: post => post.Author)
									.Include(path: post => post.Tags)
									.SingleOrDefault(predicate: post => post.Id == id && post.Slug == slug);
			
			if (selectedPost == null)
			{
				return new HttpNotFoundResult(statusDescription: "Post not found");
			}

			var tags = DbContext.Tags.ToList();

			var postViewModel = new PostDetails
			{
				Post = selectedPost,
				Tags = tags
			};
			
			return View(postViewModel);
		}
	}
}