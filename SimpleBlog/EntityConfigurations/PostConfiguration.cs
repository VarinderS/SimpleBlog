using System.Data.Entity.ModelConfiguration;
using SimpleBlog.Models;

namespace SimpleBlog.EntityConfigurations
{
	public class PostConfiguration : EntityTypeConfiguration<Post>
	{
		public PostConfiguration()
		{
			Property(propertyExpression: p => p.Title).IsRequired().HasMaxLength(value: 128);

			Property(propertyExpression: p => p.Slug).HasMaxLength(value: 256);
		}
	}
}