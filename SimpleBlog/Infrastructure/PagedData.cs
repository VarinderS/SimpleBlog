using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleBlog.Infrastructure
{
	public class PagedData<T> : IEnumerable<T> where T : class
	{
		private readonly IEnumerable<T> _items;

		public int PageNumber { get; set; }

		public int TotalPages { get; set; }

		public bool HasNextPage { get; set; }

		public bool HasPreviousPage { get; set; }

		public int NextPage
		{
			get
			{
				if (HasNextPage == false)
				{
					throw new InvalidOperationException();
				}

				return PageNumber + 1;
			}
		}

		public int PreviousPage
		{
			get
			{
				if (HasPreviousPage == false)
				{
					throw new InvalidOperationException();
				}

				return PageNumber - 1;
			}
		}

		public PagedData(IEnumerable<T> items, int pageSize, int pageNumber, int totalItems)
		{
			_items = items;

			PageNumber = pageNumber;
			
			TotalPages = (int)Math.Ceiling((float)totalItems / pageSize);

			HasNextPage = pageNumber < TotalPages;

			HasPreviousPage = pageNumber > 1;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}