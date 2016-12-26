using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Controllers;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using System.Collections.Generic;
using SimpleBlog.Extensions;

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


		[HttpPost, ValidateAntiForgeryToken, ValidateInput(enableValidation: false)]
		public ActionResult Create(PostCreate form)
		{
			if (ModelState.IsValid == false)
			{
				return View(form);
			}

			var currentUser = UserManager.FindById(userId: User.Identity.GetUserId());


			//var newTags = form.Tags.Where(predicate: tag => tag.Id == null).Select(selector: tag => new Tag { Name = tag.Name });

			//if (newTags.Any())
			//{
			//	DbContext.Tags.AddRange(entities: newTags);

			//	DbContext.SaveChanges();
			//}


			//var selectedTagIds = form.Tags.Where(predicate: tag => tag.IsChecked == true).Select(selector: tag => tag.Id);

			var tags = ReconsileTags(form.Tags).ToList(); // DbContext.Tags.Where(predicate: tag => selectedTagIds.Contains(tag.Id)).ToList();

			var post = new Post
			{
				Title = form.Title,
				Description = form.Description,
				Author = currentUser,
				DatePosted = DateTime.Now,
				Slug = form.Slug,
				Tags = tags
			};

			DbContext.Posts.Add(entity: post);

			DbContext.SaveChanges();

			return RedirectToAction(actionName: "Index");
		}
		

		public ActionResult Edit(int id = 0)
		{
			if (id == 0)
			{
				return new HttpNotFoundResult(statusDescription: "Unable to find post");
			}

			var selectedPost = DbContext.Posts.Include(path: "Tags").Single(predicate: post => post.Id == id);

			if (selectedPost == null)
			{
				return new HttpNotFoundResult(statusDescription: "Unable to find post");
			}

			var selectedTagIds = selectedPost.Tags.Select(selector: tag => tag.Id).ToArray();

			var tagList = DbContext.Tags.Select(selector: tag => new TagCheckbox { Id = tag.Id, Name = tag.Name, IsChecked = selectedTagIds.Contains(tag.Id) }).ToList();

			var postEditViewModel = new PostEdit
			{
				Title = selectedPost.Title,
				Slug = selectedPost.Slug,
				Description = selectedPost.Description,
				Tags = tagList
			};

			return View(postEditViewModel);
		}

		[HttpPost, ValidateAntiForgeryToken, ValidateInput(enableValidation: false)]
		public ActionResult Edit(PostEdit form)
		{
			if (ModelState.IsValid == false)
			{
				return View(form);
			}

			var selectedPost = DbContext.Posts.Include("Tags").Include(path: "Author").Single(predicate: post => post.Id == form.Id);

			var currentUserId = User.Identity.GetUserId();

			if (currentUserId != selectedPost.Author.Id)
			{
				ModelState.AddModelError(key: "", errorMessage: "Not authorized to update the post");

				return View(form);
			}


			var selectedTagIds = form.Tags.Where(predicate: tag => tag.IsChecked == true).Select(selector: tag => tag.Id).ToArray();

			var tags = ReconsileTags(tags: form.Tags).ToList();

			selectedPost.Title = form.Title;
			selectedPost.Description = form.Description;
			selectedPost.Tags = tags;
			selectedPost.Slug = form.Slug;

			DbContext.SaveChanges();

			return RedirectToAction(actionName: "Index");
		}
		

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Delete(int id = 0)
		{
			if (id == 0)
			{
				return new HttpNotFoundResult(statusDescription: "Post not found");
			}

			var post = DbContext.Posts.Include(path: "Author").SingleOrDefault(predicate: p => p.Id == id);

			if (post == null)
			{
				return new HttpNotFoundResult(statusDescription: "Post not found");
			}

			var currentUserId = User.Identity.GetUserId();

			if (currentUserId != post.Author.Id)
			{
				return new HttpNotFoundResult(statusDescription: "Not authorized to delee post");
			}


			post.DateDeleted = DateTime.Now;

			DbContext.SaveChanges();

			return RedirectToAction(actionName: "Index");
		}


		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Recover(int id = 0)
		{
			if (id == 0)
			{
				return new HttpNotFoundResult(statusDescription: "Post not found");
			}

			var post = DbContext.Posts.Include(path: "Author").SingleOrDefault(predicate: p => p.Id == id);

			if (post == null)
			{
				return new HttpNotFoundResult(statusDescription: "Post not found");
			}

			var currentUserId = User.Identity.GetUserId();

			if (currentUserId != post.Author.Id)
			{
				return new HttpNotFoundResult(statusDescription: "Not authorized to delee post");
			}


			post.DateDeleted = null;

			DbContext.SaveChanges();

			return RedirectToAction(actionName: "Index");
		}
		
		private IEnumerable<Tag> ReconsileTags(IEnumerable<TagCheckbox> tags)
		{
			foreach (var tag in tags.Where(t => t.IsChecked))
			{
				if (tag.Id != null)
				{
					yield return DbContext.Tags.Single(predicate: t => t.Id == tag.Id);

					continue;
				}

				var existingTag = DbContext.Tags.SingleOrDefault(predicate: t => t.Name == tag.Name);

				if (existingTag != null)
				{
					yield return existingTag;

					continue;
				}


				var newTag = new Tag 
				{ 
					Name = tag.Name,
					Slug = tag.Name.Sluggify()
				};

				DbContext.Tags.Add(entity: newTag);

				DbContext.SaveChanges();

				yield return newTag;
			}
		}
	}
}