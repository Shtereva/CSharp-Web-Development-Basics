namespace MeTube.App.Controllers
{
    using Data;
    using SimpleMvc.Framework.Controllers;
    public abstract class BaseController : Controller
    {
        protected MeTubeDbContext Context;

        protected BaseController()
        {
            this.Context = new MeTubeDbContext();
        }

        public override void OnAuthentication()
        {
            this.Model.Data["topMenu"] = this.User.IsAuthenticated ?
                @"<li class=""nav-item active"">
	                <a class=""nav-link"" href=""/"">Home</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/user/profile"">Profile</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/tubes/upload"">Upload</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/user/logout"">Logout</a>
                </li>" :
                @"<li class=""nav-item active"">
	                <a class=""nav-link"" href=""/"">Home</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/user/login"">Login</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/user/register"">Register</a>
                </li>";
        }
    }
}
