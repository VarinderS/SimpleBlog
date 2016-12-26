using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using System.Collections.Generic;

namespace SimpleBlog.ViewModels
{
	public class HomeIndex
	{
		public PagedData<Post> Posts { get; set; }

		public IList<Tag> Tags { get; set; }
	}
}