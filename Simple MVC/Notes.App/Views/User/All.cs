namespace Notes.App.Views.User
{
    using ViewModels;
    using SimpleMvc.Framework.Contracts.Generic;
    using System.Text;
    public class All : IRenderable<AllUsersViewModel>
    {
        public string Render()
        {
            var sb = new StringBuilder();

            sb.AppendLine("<h1>All Users</h1>");
            sb.AppendLine($@"<ul></br>");

            foreach (var username in this.Model.Usernames)
            {
                sb.AppendLine($@"<li><a href=""/user/profile?id={username.UserId}"">{username.Username}</a></li></br>");
            }

            sb.AppendLine($@"</ul>");
            return sb.ToString().Trim();
        }

        public AllUsersViewModel Model { get; set; }
    }
}
