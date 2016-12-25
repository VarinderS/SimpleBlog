using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Controllers;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;

namespace SimpleBlog.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	public class PostController : BaseController
	{
		public ActionResult Index(int pageNumber = 1)
		{
			var pageSize = 5;

			var totalPosts = DbContext.Posts.Count();

			var postIds = DbContext.Posts
									.OrderBy(keySelector: post => post.Title)
									.Skip(count: (pageSize * (pageNumber - 1)))
									.Take(count: pageSize)
									.Select(selector: post => post.Id)
									.ToArray();

			var posts = DbContext.Posts
								.Include(path: "Author")
								.Where(predicate: post => postIds.Contains(post.Id))
								.ToList();

			var pagedPosts = new PagedData<Post>(items: posts, pageSize: pageSize, pageNumber: pageNumber, totalItems: totalPosts);

			var postsViewModel = new PostIndex { Posts = pagedPosts };

			return View(postsViewModel);

			//var posts = DbContext.Posts.ToList();

			//return View(posts);	
		}


		public ActionResult Create()
		{
			return View();
		}


		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Create(PostCreate form)
		{
			if (ModelState.IsValid == false)
			{
				return View(form);
			}

			var currentUser = UserManager.FindById(userId: User.Identity.GetUserId());

			var post = new Post
			{
				Title = form.Title,
				Description = form.Description,
				Author = currentUser,
				DatePosted = DateTime.Now
			};

			DbContext.Posts.Add(entity: post);

			DbContext.SaveChanges();

			return RedirectToAction(actionName: "Index");
		}
	}
}