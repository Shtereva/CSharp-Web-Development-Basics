namespace Chushka.App.Controllers
{
    using Data;
    using SoftUni.WebServer.Mvc.Controllers;

    public abstract class BaseController : Controller
    {
        protected ChushkaDbContext Context;

        protected BaseController()
        {
            this.Context = new ChushkaDbContext();
        }

        public override void OnAuthentication()
        {
            bool role = this.User.Roles != null && this.User.IsInRole("1");

            string userRoleResult = role ?
                @"<li class=""nav-item"">
                <a class=""nav-link nav-link-white"" href=""/"">Home</a>
                </li>
                <li class=""nav-item"">
                <a class=""nav-link nav-link-white"" href=""/products/create"">Create Product</a>
                </li>
                <li class=""nav-item"">
                <a class=""nav-link nav-link-white"" href=""/orders/all"">All Orders</a>
                </li>
                <a class=""nav-link nav-link-white"" href=""/"">Logout</a>
                </li>" :
                @"<li class=""nav-item"">
                <a class=""nav-link nav-link-white"" href=""/"">Home</a>
                </li>
                <li class=""nav-item"">
                <a class=""nav-link nav-link-white"" href=""/"">Logout</a>
                </li>";

            this.ViewData.Data["topMenu"] = this.User.IsAuthenticated ?
                userRoleResult :
                @"<li class=""nav-item"">
                <a class=""nav-link nav-link-white"" href=""/"">Home</a>
                </li>
                <li class=""nav-item"">
                <a class=""nav-link nav-link-white"" href=""/user/login"">Login</a>
                </li>
                <li class=""nav-item"">
                <a class=""nav-link nav-link-white"" href=""/user/register"">Register</a>
                </li>";
        }
    }
}
