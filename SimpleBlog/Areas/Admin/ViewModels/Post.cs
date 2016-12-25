using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;

namespace SimpleBlog.Areas.Admin.ViewModels
{
	public class PostIndex
	{
		public PagedData<Post> Posts { get; set; }
	}

	public class PostCreate
	{
		[Required, MaxLength(length: 128, ErrorMessage = "Title cannot be more than 128 letters")]
		public string Title { get; set; }

		public string Description { get; set; }

		public IList<TagCheckbox> Tags { get; set; }
	}

	public class PostEdit
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public IList<TagCheckbox> Tags { get; set; }
	}

	public class TagCheckbox
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public bool IsChecked { get; set; }
	}
}