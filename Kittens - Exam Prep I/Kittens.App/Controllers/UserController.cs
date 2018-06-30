namespace Kittens.App.Controllers
{
    using System.Linq;
    using Kittens.Models;
    using Models;
    using SimpleMvc.Common;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Attributes.Security;
    using SimpleMvc.Framework.Interfaces;

    public class UserController : BaseController
    {
        [HttpGet]
        public IActionResult Login()
        {
            this.Model.Data["error"] = string.Empty;
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserModel model)
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
            this.Model.Data["error"] = string.Empty;
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserModel model)
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


            return this.RedirectToAction("/");
        }

        [HttpGet]
        [PreAuthorize]
        public IActionResult Logout()
        {
            this.SignOut();

            return this.RedirectToAction("/");
        }
    }
}
