using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SimpleBlog.Models;
using SimpleBlog.ViewModels;

namespace SimpleBlog.Controllers
{
	public class AccountController : BaseController
	{
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Register(AccountRegister form)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser { UserName = form.Name, Email = form.Email };

				var result = UserManager.Create(user: user, password: form.Password);

				if (result.Succeeded)
				{
					SignInManager.SignIn(user: user, isPersistent: false, rememberBrowser: false);

					return RedirectToAction("Index", "Home");
				}

				AddErrors(result: result);
			}

			return View(form);
		}

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Login(AccountLogin form, string returnUrl)
		{
			if (ModelState.IsValid == false)
			{
				return View();
			}


			var user = UserManager.FindByName(userName: form.UserName);

			if (user == null)
			{
				ModelState.AddModelError(key: "UserName", errorMessage: "Account invalid");

				return View();
			}


			var result = SignInManager.PasswordSignIn(userName: user.UserName, password: form.Password, isPersistent: form.RememberMe, shouldLockout: false);

			if (result != SignInStatus.Success)
			{
				ModelState.AddModelError(key: "", errorMessage: "Incorrect username or password");

				return View();
			}

			return RedirectToLocal(url: returnUrl);
        }


		public ActionResult Logout()
		{
			AuthenticationManager.SignOut(authenticationTypes: DefaultAuthenticationTypes.ApplicationCookie);

			return RedirectToAction(actionName: "Index", controllerName: "Home");
		}


		private ActionResult RedirectToLocal(string url)
		{
			if (Url.IsLocalUrl(url: url))
			{
				return Redirect(url: url);
			}

			return RedirectToAction(actionName: "Index", controllerName: "Home");
		}

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error);
			}
		}
	}
}