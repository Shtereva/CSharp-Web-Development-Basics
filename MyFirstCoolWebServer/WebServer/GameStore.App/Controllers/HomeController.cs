namespace HTTPServer.GameStore.App.Controllers
{
    using Server.Http.Contracts;
    public class HomeController : BaseController
    {
        public HomeController(IHttpRequest req) : base(req)
        {
        }
        public IHttpResponse Index()
        {
            return this.FileViewResponse(@"/home/index");
        }
    }
}
