﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimpleBlog.Models;

namespace SimpleBlog.Areas.Admin.ViewModels
{
	public class PostIndex
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public IList<Tag> Tags { get; set; }
	}

	public class PostCreate
	{
		[Required, MaxLength(length: 128, ErrorMessage = "Title cannot be more than 128 letters")]
		public string Title { get; set; }

		public string Description { get; set; }

		public IList<Tag> Tags { get; set; }
	}
}