using System.CodeDom;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleBlog.EntityConfigurations;

namespace SimpleBlog.Models
{
	public class SimpleBlogDbContext : IdentityDbContext<ApplicationUser>
	{
		public SimpleBlogDbContext() : base(nameOrConnectionString: "SimpleBlogDbContext") { }


		public static SimpleBlogDbContext Create()
		{
			return new SimpleBlogDbContext();
		}


		public DbSet<Post> Posts { get; set; }

		public DbSet<Tag> Tags { get; set; }


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Configurations.Add(entityTypeConfiguration: new PostConfiguration());

			modelBuilder.Configurations.Add(entityTypeConfiguration: new TagConfiguration());
		}
	}
}