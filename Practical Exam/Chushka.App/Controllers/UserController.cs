namespace Chushka.App.Controllers
{
    using System;
    using System.Linq;
    using Chushka.Models;
    using Models.BindingModels;
    using SoftUni.WebServer.Common;
    using SoftUni.WebServer.Mvc.Attributes.HttpMethods;
    using SoftUni.WebServer.Mvc.Attributes.Security;
    using SoftUni.WebServer.Mvc.Interfaces;


    public class UserController : BaseController
    {
        private int roleId = 2;

        [HttpGet]
        public IActionResult Login()
        {
            this.ViewData["error"] = string.Empty;
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserBindingModel model)
        {
            using (this.Context)
            {
                var passHash = PasswordUtilities.GetPasswordHash(model.Password);

                var user = this.Context.Users.SingleOrDefault(u =>
                    u.Username == model.Username && u.PasswordHash == passHash);

                if (user == null)
                {
                    this.ViewData["error"] = "Invalid username or password";
                    return this.View();
                }

                var roles = new string[] { user.RoleId.ToString() }; // ????

                this.SignIn(user.Username, user.Id, roles);

                var a = this.User.IsAuthenticated;
            }

            return this.RedirectToAction("/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            this.ViewData["error"] = string.Empty;
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel model)
        {
            if (!this.IsValidModel(model) || model.Password != model.ConfirmPassword)
            {
                this.ViewData["error"] = "Invalid credentials";
                return this.View();
            }

            using (this.Context)
            {
                if (this.Context.Users.Any(u => u.Username == model.Username))
                {
                    this.ViewData["error"] = "User already exist";
                    return this.View();
                }

                if (!this.Context.Users.Any())
                {
                    this.roleId = 1;
                }
                var user = new User()
                {
                    Username = model.Username,
                    Email = model.Email,
                    FullName = model.FullName,
                    PasswordHash = PasswordUtilities.GetPasswordHash(model.Password),
                    RoleId = this.roleId
                };

                this.Context.Add(user);
                this.Context.SaveChanges();
            }

            return this.RedirectToAction("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            this.SignOut();

            return this.RedirectToAction("/");
        }
    }
}
