namespace Notes.App.Views.User
{
    using System.Text;
    using SimpleMvc.Framework.Contracts;
    public class Register : IRenderable
    {
        public string Render()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"<h1>Register</h1>");
            sb.AppendLine($@"<form action=""register"" method=""post""></br>");
            sb.AppendLine($@"<input type=""text"" name=""Username"" placeholder=""Username""></br>");
            sb.AppendLine($@"<input type=""password"" name=""Password"" placeholder=""Password""></br>");
            sb.AppendLine($@"<input type=""submit"" value=""Register""></br>");
            sb.AppendLine($"</form></br>");

            return sb.ToString().Trim();
        }
    }
}
