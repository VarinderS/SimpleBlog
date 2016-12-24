using System.Linq.Expressions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleBlog.Models;

namespace SimpleBlog.Migrations
{
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
        }
    }
}
