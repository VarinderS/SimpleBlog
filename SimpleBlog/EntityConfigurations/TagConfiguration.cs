using System.Data.Entity.ModelConfiguration;
using SimpleBlog.Models;

namespace SimpleBlog.EntityConfigurations
{
	public class TagConfiguration : EntityTypeConfiguration<Tag>
	{
		public TagConfiguration()
		{
			Property(propertyExpression: tag => tag.Name).IsRequired().HasMaxLength(value: 128);

			Property(propertyExpression: tag => tag.Slug).HasMaxLength(value: 256);
		}
	}
}