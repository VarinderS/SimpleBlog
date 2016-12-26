using SimpleBlog.Models;
using System.Collections.Generic;

namespace SimpleBlog.ViewModels
{
	public class PostDetails
	{
		public Post Post { get; set; }

		public IList<Tag> Tags { get; set; }
	}
}