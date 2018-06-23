using System.Reflection;

namespace SimpleMvc.Framework
{
    using System;
    using WebServer;
    public static class MvcEngine
    {
        public static void Run(WebServer webServer)
        {
            MvcContext.Get.AssemblyName = Assembly.GetEntryAssembly().GetName().Name;

            try
            {
                webServer.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
