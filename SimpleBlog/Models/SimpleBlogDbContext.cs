using System.CodeDom;
using System.Data.Entity;
using SimpleBlog.EntityConfigurations;

namespace SimpleBlog.Models
{
	public class SimpleBlogDbContext : DbContext
	{
		public SimpleBlogDbContext()
		{
			
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