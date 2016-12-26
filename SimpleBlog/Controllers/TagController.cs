using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using SimpleBlog.ViewModels;

namespace SimpleBlog.Controllers
{
	public class TagController : BaseController
	{
		public ActionResult Index(int id, string slug, int pageNumber = 1)
		{
			var pageSize = 2;

			if (id == 0)
			{
				return new HttpNotFoundResult(statusDescription: "Tag not found");
			}

			var selectedTag = DbContext.Tags
									.OrderBy(keySelector: tag => tag.Name)
									.Include(path: tag => tag.Posts)
									.SingleOrDefault(predicate: tag => tag.Id == id && tag.Slug == slug);
			
			if (selectedTag == null)
			{
				return new HttpNotFoundResult(statusDescription: "Tag not found");
			}

			var totalPosts = selectedTag.Posts.Count();

			var postIds = selectedTag.Posts.Select(selector: post => post.Id).ToArray();

			var posts = DbContext.Posts.OrderBy(keySelector: post => post.DatePosted)
										.Include(path: post => post.Tags)
										.Include(path: post => post.Author)
										.Where(predicate: post => post.DateDeleted == null)
										.Where(predicate: post => postIds.Contains(post.Id))
										.Skip(pageSize * (pageNumber - 1))
										.Take(count: pageSize)
										.ToList();

			var pagedPosts = new PagedData<Post>(items: posts, pageSize: pageSize, pageNumber: pageNumber, totalItems: totalPosts);

			var tags = DbContext.Tags.ToList();

			var tagIndexViewModel = new TagIndex
			{
				Posts = pagedPosts,
				Tags = tags,
				Tag = selectedTag
			};

			return View(tagIndexViewModel);
		}
	}
}