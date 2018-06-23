namespace SimpleMvc.App
{
    using Framework;
    using Framework.Routes;
    using WebServer;
    public class Launcher
    {
        public static void Main()
        {
            var server = new WebServer(8000, new ControllerRouter());
            MvcEngine.Run(server);
        }
    }
}
