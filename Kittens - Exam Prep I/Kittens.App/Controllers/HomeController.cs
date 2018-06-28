namespace Kittens.App.Controllers
{
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Interfaces;

    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            this.Model["logged"] = "block";
            this.Model["logged-out"] = "none";

            return this.View();
        }
    }
}
