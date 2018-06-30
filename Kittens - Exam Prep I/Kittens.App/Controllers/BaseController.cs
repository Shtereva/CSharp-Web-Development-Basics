namespace Kittens.App.Controllers
{
    using Data;
    using SimpleMvc.Framework.Controllers;
    public abstract class BaseController : Controller
    {
        protected KittenDbContext Context;

        protected BaseController()
        {
            this.Context = new KittenDbContext();
            this.OnAuthentication();
        }
    }
}
