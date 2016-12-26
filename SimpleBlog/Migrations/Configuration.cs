using System.Linq.Expressions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleBlog.Models;

namespace SimpleBlog.Migrations
{
	using Extensions;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<SimpleBlog.Models.SimpleBlogDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SimpleBlog.Models.SimpleBlogDbContext context)
        {

			var roleStore = new RoleStore<IdentityRole>(context: context);

			var roleManager = new RoleManager<IdentityRole>(store: roleStore);

			var userStore = new UserStore<ApplicationUser>(context: context);

			var userManager = new UserManager<ApplicationUser>(store: userStore);


			var publicRole = roleManager.FindByName(roleName: "Public");

			if (publicRole == null)
			{
				publicRole = new IdentityRole { Name = "Public" };

				roleManager.Create(role: publicRole);
			}


			var adminRole = roleManager.FindByName(roleName: "Admin");

			if (adminRole == null)
			{
				adminRole = new IdentityRole { Name = "Admin" };

				roleManager.Create(role: adminRole);
			}


			var adminUser = userManager.FindByName(userName: "Varinder");

			if (adminUser == null)
			{
				adminUser = new ApplicationUser { UserName = "Varinder", Email = "varinder@email.com" };

				userManager.Create(user: adminUser, password: "password");
			}


			var adminUserCreated = userManager.IsInRole(userId: adminUser.Id, role: adminRole.Name);

			if (adminUserCreated == false)
			{
				userManager.AddToRole(userId: adminUser.Id, role: adminRole.Name);
			}


			context.Tags.AddOrUpdate(
				tag => tag.Name,
				new Tag { Name = "C#" },
				new Tag { Name = "Random" }
			);

			context.SaveChanges();
			

			context.Posts.AddOrUpdate(
				post => post.Title,
				new Post { Title = "Test Post", DatePosted = DateTime.Now, Author = adminUser },
				new Post { Title = "Test Post 2", DatePosted = DateTime.Now, Author = adminUser, Tags = context.Tags.Take(2).ToList() },
				new Post { Title = "Test Post 3", DatePosted = DateTime.Now, Author = adminUser, Tags = context.Tags.Take(1).ToList() },
				new Post { Title = "Test Post 4", DatePosted = DateTime.Now, Author = adminUser, Tags = context.Tags.Take(2).ToList() },
				new Post { Title = "Test Post 5", DatePosted = DateTime.Now, Author = adminUser, Tags = context.Tags.Take(1).ToList() },
				new Post { Title = "Test Post 6", DatePosted = DateTime.Now, Author = adminUser, Tags = context.Tags.Take(2).ToList() },
				new Post { Title = "Test Post 7", DatePosted = DateTime.Now, Author = adminUser, Tags = context.Tags.Take(1).ToList() },
				new Post { Title = "Test Post 8", DatePosted = DateTime.Now, Author = adminUser, Tags = context.Tags.Take(2).ToList() },
				new Post { Title = "Test Post 9", DatePosted = DateTime.Now, Author = adminUser, Tags = context.Tags.Take(1).ToList() },
				new Post { Title = "Test Post 10", DatePosted = DateTime.Now, Author = adminUser, Tags = context.Tags.Take(2).ToList() },
				new Post { Title = "Test Post 11", DatePosted = DateTime.Now, Author = adminUser, Tags = context.Tags.Take(1).ToList() },
				new Post { Title = "Test Post 12", DatePosted = DateTime.Now, Author = adminUser, Tags = context.Tags.Take(2).ToList() }
			);


			foreach (var tag in context.Tags)
			{
				if (string.IsNullOrEmpty(tag.Slug))
				{
					tag.Slug = tag.Name.Sluggify();
				}
			}


			foreach (var post in context.Posts)
			{
				if (string.IsNullOrEmpty(post.Slug))
				{
					post.Slug = post.Title.Sluggify();
				}
			}


			context.SaveChanges();
        }
    }
}
