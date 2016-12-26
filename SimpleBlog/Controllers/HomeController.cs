using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using SimpleBlog.ViewModels;

namespace SimpleBlog.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index(int pageNumber = 1)
        {
			var pageSize = 15;

			var totalPosts = DbContext.Posts.Count();

			var postIds = DbContext.Posts
									.Where(predicate: post => post.DateDeleted == null)
									.OrderByDescending(keySelector: post => post.DatePosted)
									.Skip(count: pageSize * (pageNumber - 1))
									.Take(count: pageSize)
									.Select(selector: post => post.Id)
									.ToArray();	

			var posts = DbContext.Posts
								.Include(path: post => post.Author)
								.Include(path: post => post.Tags)
								.Where(predicate: post => postIds.Contains(post.Id))
								.ToList();
			
			var pagedPosts = new PagedData<Post>(items: posts, pageSize: pageSize, pageNumber: pageNumber, totalItems: totalPosts);


			var tags = DbContext.Tags.OrderBy(keySelector: tag => tag.Name).ToList();

			var postIndexViewModel = new HomeIndex
			{
				Posts = pagedPosts,
				Tags = tags
			};

            return View(postIndexViewModel);
        }
    }
}