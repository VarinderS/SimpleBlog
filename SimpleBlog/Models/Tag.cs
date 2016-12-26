using System.Collections.Generic;

namespace SimpleBlog.Models
{
	public class Tag
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Slug { get; set; }

		public IList<Post> Posts { get; set; }

		public Tag()
		{
			Posts = new List<Post>();
		}
	}
}