using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using System.Collections.Generic;

namespace SimpleBlog.ViewModels
{
	public class TagIndex
	{
		public Tag Tag { get; set; }

		public PagedData<Post> Posts { get; set; }

		public IList<Tag> Tags { get; set; }
	}
}