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
								.Include(path: "Tags")
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
			var tags = DbContext.Tags.OrderBy(keySelector: tag => tag.Name).Select(selector: tag => new TagCheckbox { Id = tag.Id, Name = tag.Name }).ToList();

            var createPostViewModel = new PostCreate
			{
				Tags = tags
			};

			return View(createPostViewModel);
		}


		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Create(PostCreate form)
		{
			if (ModelState.IsValid == false)
			{
				return View(form);
			}

			var currentUser = UserManager.FindById(userId: User.Identity.GetUserId());

			var selectedTagIds = form.Tags.Where(predicate: tag => tag.IsChecked == true).Select(selector: tag => tag.Id);

			var tags = DbContext.Tags.Where(predicate: tag => selectedTagIds.Contains(tag.Id)).ToList();

			var post = new Post
			{
				Title = form.Title,
				Description = form.Description,
				Author = currentUser,
				DatePosted = DateTime.Now,
				Tags = tags
			};

			DbContext.Posts.Add(entity: post);

			DbContext.SaveChanges();

			return RedirectToAction(actionName: "Index");
		}
	}
}