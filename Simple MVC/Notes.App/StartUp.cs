namespace Notes.App
{
    using Data;
    using WebServer;
    using SimpleMvc.Framework;
    using SimpleMvc.Framework.Routes;
    using Microsoft.EntityFrameworkCore;
    public class StartUp
    {
        public static void Main()
        {
            InitializeDb();

            var server = new WebServer(8000, new ControllerRouter(), new ResourceRouter());
            MvcEngine.Run(server);
        }

        private static void InitializeDb()
        {
            var db = new NotesDbContext();
            db.Database.Migrate();
        }
    }
}
