using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Controllers;
using SimpleBlog.Models;

namespace SimpleBlog.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	public class PostController : BaseController
	{
		public ActionResult Index()
		{
			var posts = DbContext.Posts.Include("Author").ToList();

			return View(posts);
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