namespace Kittens.App.Controllers
{
    using Data;
    using Kittens.Models;
    using Models;
    using SimpleMvc.Common;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Interfaces;

    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserModel model)
        {
            if (!this.IsValidModel(model))
            {
                return this.RedirectToAction("user/register");
            }

            return this.View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserModel model)
        {
            if (!this.IsValidModel(model) || model.Password != model.ConfirmPassword)
            {
                return this.RedirectToAction("user/register");
            }

            using (var db = new KittenDbContext())
            {
                var user = new User()
                {
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = PasswordUtilities.GetPasswordHash(model.Password)
                };

                db.Add(user);
                db.SaveChanges();
            }

            return this.RedirectToAction("/");
        }

        [HttpGet]
        public IActionResult Logout()
        {
           return this.RedirectToAction("/");
        }
    }
}
