using SimpleBlog.Controllers;
using System.Web.Mvc;
using System.Linq;
using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace SimpleBlog.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	public class UserController : BaseController
	{
		public ActionResult Index(int pageNumber = 1)
		{
			var pageSize = 5;

			var totalUsers = UserManager.Users.Count();

			var users = UserManager.Users
								.OrderBy(keySelector: user => user.UserName)
								.Skip(count: pageSize * (pageNumber - 1))
								.Take(count: pageSize)
								.ToList();

			var userWithRolesList = new List<UserWithRoles>();

			foreach (var user in users)
			{
				var roleIds = user.Roles.Select(selector: userRole => userRole.RoleId).ToArray();

				var roles = RoleManager.Roles.Where(predicate: role => roleIds.Contains(role.Id)).ToList();

				var userWithRoles = new UserWithRoles
				{
					User = user,
					Roles = roles
				};

				userWithRolesList.Add(item: userWithRoles);
			}
			
			var pagedUsers = new PagedData<UserWithRoles>(items: userWithRolesList, pageSize: pageSize, pageNumber: pageNumber, totalItems: totalUsers);

			var usersViewModel = new UserIndex { UsersWithRoles = pagedUsers };
			
			return View(usersViewModel);
		}


		public ActionResult Add()
		{
			var roles = RoleManager.Roles.Select(selector: role => new RoleCheckbox { Id = role.Id, Name = role.Name, IsChecked = false }).ToList();

			var userAddViewModel = new UserAdd
			{
				Roles = roles
			};
			
			return View(userAddViewModel);
		}


		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Add(UserAdd form)
		{
			if (ModelState.IsValid == false)
			{
				return View(form);
			}

			var user = new ApplicationUser
			{
				UserName = form.UserName,
				FullName = form.FullName,
				Email = form.Email
			};

			var result = UserManager.Create(user: user, password: form.Password);

			if (result.Succeeded == false)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(key: "", errorMessage: error);
				}

				return View(form);
			}

			foreach (var roleCheckbox in form.Roles)
			{
				if (roleCheckbox.IsChecked)
				{
					if (UserManager.IsInRole(userId: user.Id, role: roleCheckbox.Name) == false)
					{
						UserManager.AddToRole(userId: user.Id, role: roleCheckbox.Name);
					}
				}
				else
				{
					UserManager.RemoveFromRole(userId: user.Id, role: roleCheckbox.Name);
				}
			}

			return RedirectToAction(actionName: "Index");
		}


		public ActionResult Edit(string id = "")
		{
			if (string.IsNullOrEmpty(id))
			{
				return new HttpNotFoundResult(statusDescription: "User not found");
			}

			var user = UserManager.FindById(userId: id);

			if (user == null)
			{
				return new HttpNotFoundResult(statusDescription: "User not found");
			}

			var userRoleIds = user.Roles.Select(selector: userRole => userRole.RoleId).ToArray();

			var roles = RoleManager.Roles.Select(selector: role => new RoleCheckbox { Id = role.Id, Name = role.Name, IsChecked = userRoleIds.Contains(role.Id) }).ToList();

			var userEditViewModel = new UserEdit
			{
				Id = user.Id,
				UserName = user.UserName,
				FullName = user.FullName,
				Email = user.Email,
				Roles = roles
			};

			return View(userEditViewModel);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Edit(UserEdit form)
		{
			if (ModelState.IsValid == false)
			{
				return View(form);
			}

			var user = UserManager.FindById(userId: form.Id);

			if (user == null)
			{
				return new HttpNotFoundResult(statusDescription: "User not found");
			}

			user.UserName = form.UserName;

			user.FullName = form.FullName;

			foreach (var roleCheckbox in form.Roles)
			{
				if (roleCheckbox.IsChecked)
				{
					if (UserManager.IsInRole(userId: user.Id, role: roleCheckbox.Name) == false)
					{
						UserManager.AddToRole(userId: user.Id, role: roleCheckbox.Name);
					}
				}
				else
				{
					UserManager.RemoveFromRole(userId: user.Id, role: roleCheckbox.Name);
				}
			}

			UserManager.Update(user: user);

			return RedirectToAction(actionName: "Index");
		}
	}
}