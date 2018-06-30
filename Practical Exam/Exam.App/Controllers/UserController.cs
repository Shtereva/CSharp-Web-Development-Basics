namespace MeTube.App.Controllers
{
    using System;
    using System.Linq;
    using MeTube.Models;
    using Models.BindingModels;
    using SimpleMvc.Common;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Attributes.Security;
    using SimpleMvc.Framework.Interfaces;

    public class UserController : BaseController
    {
        [HttpGet]
        public IActionResult Login()
        {
            if (this.User.IsAuthenticated)
            {
                return this.RedirectToAction("/");
            }

            this.Model.Data["error"] = string.Empty;
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
                    this.Model.Data["error"] = "Invalid username or password";
                    return this.View();
                }

                this.SignIn(user.Username, user.Id);
            }

            return this.RedirectToAction("/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (this.User.IsAuthenticated)
            {
                return this.RedirectToAction("/");
            }

            this.Model.Data["error"] = string.Empty;
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel model)
        {
            if (!this.IsValidModel(model) || model.Password != model.ConfirmPassword)
            {
                this.Model.Data["error"] = "Invalid credentials";
                return this.View();
            }

            using (this.Context)
            {
                if (this.Context.Users.Any(u => u.Username == model.Username))
                {
                    this.Model.Data["error"] = "User already exist";
                    return this.View();
                }

                var user = new User()
                {
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = PasswordUtilities.GetPasswordHash(model.Password)
                };

                this.Context.Add(user);
                this.Context.SaveChanges();
            }


            return this.RedirectToAction("/user/login");
        }

        [HttpGet]
        [PreAuthorize]
        public IActionResult Logout()
        {
            this.SignOut();

            return this.RedirectToAction("/");
        }

        [HttpGet]
        [PreAuthorize]
        public IActionResult Profile()
        {
            var videos = this.Context.Tubes
                .Select(t => $@"<tr class=""table-warning"">
                <th scope=""row"">{t.Id}</th>
                <td>{t.Title}</td>
                <td>{t.Author}</td>
                <td>
                <a href=""/tubes/details?id={t.Id}"" class=""btn btn-primary btn-sm"">Details</a>
                </td>
                </tr>")
                .ToArray();

            this.Model.Data["videos"] = string.Join(Environment.NewLine, videos);
            
            return this.View();
        }
    }
}
