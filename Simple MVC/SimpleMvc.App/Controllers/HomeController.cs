using SimpleMvc.Framework.Contracts;

namespace SimpleMvc.App.Controllers
{
    using SimpleMvc.Framework.Controllers;
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
