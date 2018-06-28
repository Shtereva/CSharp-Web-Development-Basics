namespace Kittens.App.Controllers
{
    using Models;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Interfaces;

    public class KittensController : Controller
    {
        [HttpGet]
        public IActionResult All()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddKittenModel model)
        {
            return this.View();
        }
    }
}
