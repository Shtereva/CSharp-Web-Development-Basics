namespace Chushka.App
{
    using System;
    using Chushka.Models;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using SoftUni.WebServer.Mvc;
    using SoftUni.WebServer.Mvc.Routers;
    using SoftUni.WebServer.Server;

    public class Launcher
    {
        public static void Main()
        {
            using (var db = new ChushkaDbContext())
            {
                //db.Database.Migrate();
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();
            }

            var webserver = new WebServer(8000, new ControllerRouter(), new ResourceRouter());

            MvcEngine.Run(webserver);
        }
    }
}
