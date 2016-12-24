using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SimpleBlog.Models;

namespace SimpleBlog
{
	public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
	{
		public ApplicationSignInManager(UserManager<ApplicationUser, string> userManager, IAuthenticationManager authenticationManager) 
								: base(userManager: userManager, authenticationManager: authenticationManager) { }

		public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
		{
			return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
		}

		public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
		{
			var userManager = context.Get<ApplicationUserManager>();

			var signInManager = new ApplicationSignInManager(userManager: userManager, authenticationManager: context.Authentication);

			return signInManager;
		}
	}
}