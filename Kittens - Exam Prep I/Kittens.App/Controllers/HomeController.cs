namespace Kittens.App.Controllers
{
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;

    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (this.User.IsAuthenticated)
            {
                this.Model.Data["message"] = $"Welcome, {this.User.Name}!";
            }
            else
            {
                this.Model.Data["message"] = @"<a href=""/user/login"">Login</a> to trade or <a href=""user//register"">Register</a> if you don't have an account.";
            }
            return this.View();
        }
    }
}
