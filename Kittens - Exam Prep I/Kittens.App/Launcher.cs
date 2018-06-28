namespace Kittens.App
{
    using Data;
    using SimpleMvc.Framework;
    using SimpleMvc.Framework.Routers;
    using WebServer;

    public class Launcher
    {
        public static void Main()
        {
            var webserver = new WebServer(8000, new ControllerRouter(), new ResourceRouter());

            MvcEngine.Run(webserver, new KittenDbContext());
        }
    }
}
