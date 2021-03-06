﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SimpleBlog.Models
{
	public class Post
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public DateTime DatePosted { get; set; }

		public DateTime? DateDeleted { get; set; }

		public string Slug { get; set; }

		public IList<Tag> Tags { get; set; }

		public ApplicationUser Author { get; set; }

		public bool IsDeleted
		{
			get
			{
				return DateDeleted != null;
			}
		}

		public Post()
		{
			Tags = new List<Tag>();
		}
	}
}