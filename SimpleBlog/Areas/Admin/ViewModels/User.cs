using Microsoft.AspNet.Identity.EntityFramework;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.Areas.Admin.ViewModels
{
	public class UserIndex
	{
		public PagedData<UserWithRoles> UsersWithRoles { get; set; }
	}


	public class UserWithRoles
	{
		public ApplicationUser User { get; set; }

		public IList<IdentityRole> Roles { get; set; }
	}


	public class UserEdit
	{
		public string Id { get; set; }

		public string UserName { get; set; }

		public string FullName { get; set; }

		public string Email { get; set; }

		public IList<RoleCheckbox> Roles { get; set; }
	}


	public class UserAdd
	{
		public string UserName { get; set; }

		public string FullName { get; set; }

		public string Email { get; set; }

		[Required, DataType(dataType: DataType.Password)]
		public string Password { get; set; }

		[Required, Compare(otherProperty: "Password", ErrorMessage = "Password did not match")]
		public string ConfirmPassword { get; set; }

		public IList<RoleCheckbox> Roles { get; set; }
	}


	public class RoleCheckbox
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public bool IsChecked { get; set; }
	}
}