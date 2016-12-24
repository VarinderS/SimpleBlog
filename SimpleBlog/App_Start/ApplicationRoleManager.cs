using System.Threading;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SimpleBlog.Models;

namespace SimpleBlog
{
	public class ApplicationRoleManager : RoleManager<IdentityRole>
	{
		public ApplicationRoleManager(RoleStore<IdentityRole> store) : base(store: store) { }


		public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
		{
			var dbContext = context.Get<SimpleBlogDbContext>();

			var roleStore = new RoleStore<IdentityRole>(context: dbContext);
			
			var roleManager = new ApplicationRoleManager(store: roleStore);

			return roleManager;
		}
	}
}