namespace Notes.App.Views.User
{
    using System.Text;
    using ViewModels;
    using SimpleMvc.Framework.Contracts.Generic;
    public class Profile : IRenderable<UserProfileViewModel>
    {
        public string Render()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"<h1>User: {this.Model.Username}</h1>");

            sb.AppendLine($@"<form action=""profile"" method=""post""></br>");
            sb.AppendLine($@"Title: <input type=""text"" name=""Title"" placeholder=""Title""></br>");
            sb.AppendLine($@"Content: <input type=""text"" name=""Content"" placeholder=""Content""></br>");
            sb.AppendLine($@"<input type=""hidden"" name=""UserId"" value=""{this.Model.UserId}""></br>");
            sb.AppendLine($@"<input type=""submit"" value=""Add Note""></br>");
            sb.AppendLine($"</form></br>");

            sb.AppendLine($"<h3>List of Notes</h3>");
            sb.AppendLine($@"<ul></br>");

            foreach (var note in this.Model.Notes)
            {
                sb.AppendLine($@"<li>{note.Title}</br>{note.Content}</li></br>");
            }

            sb.AppendLine($@"</ul>");
            return sb.ToString().Trim();
        }

        public UserProfileViewModel Model { get; set; }
    }
}
