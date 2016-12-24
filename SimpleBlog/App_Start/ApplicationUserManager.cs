using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SimpleBlog.Models;

namespace SimpleBlog
{
	public class ApplicationUserManager : UserManager<ApplicationUser>
	{
		public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store: store) { }


		public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
		{
			var dbContext = context.Get<SimpleBlogDbContext>();

			var userStore = new UserStore<ApplicationUser>(context: dbContext);

			var manager = new ApplicationUserManager(store: userStore);

			// Configure validation logic for usernames
			manager.UserValidator = new UserValidator<ApplicationUser>(manager)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};

			// Configure validation logic for passwords
			manager.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 6,
				RequireNonLetterOrDigit = false,
				RequireDigit = false,
				RequireLowercase = false,
				RequireUppercase = false,
			};

			// Configure user lockout defaults
			manager.UserLockoutEnabledByDefault = true;
			manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
			manager.MaxFailedAccessAttemptsBeforeLockout = 5;

			return manager;
		}
	}
}