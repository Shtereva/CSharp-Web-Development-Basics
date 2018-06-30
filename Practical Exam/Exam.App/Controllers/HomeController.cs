namespace MeTube.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.EntityFrameworkCore.Extensions.Internal;
    using Models.ViewModels;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;

    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (!this.User.IsAuthenticated)
            {
                this.Model.Data["user"] = string.Empty;

                this.Model.Data["view"] = @"<div class=""jumbotron"">
                    <p class=""h1 display-3"">Welcome to MeTube&trade;!</p>
                    <p class=""h3"">The simplest, easiest to use, most comfortable Multimedia Application.</p>
                    <hr class=""my-3"">
                    <p><a href =""/user/login"">Login</a> if you have an account or<a href=""/user/register""> Register</a> now and start tubing.</p>
                    </div>";
            }
            else
            {
                var sb = new StringBuilder();

                List<MeTubeViewModel> tubes = null;

                using (this.Context)
                {
                    tubes = this.Context.Tubes
                        .Select(t => new MeTubeViewModel()
                        {
                            YoutubeId = t.YoutubeId,
                            Title = t.Title,
                            Author = t.Author,
                        })
                        .ToList();
                }

                string endCard = this.CreateHtml(sb, tubes);

                sb.AppendLine(endCard);

                this.Model.Data["view"] = sb.ToString();
                this.Model.Data["user"] =
                    $@"<p class=""h1 display-3"">Welcome {this.User.Name}!</p><hr class=""my-3""/>";
            }

            return this.View();
        }

        private string CreateHtml(StringBuilder sb, List<MeTubeViewModel> tubes)
        {

            var startCard = $@"<div class=""card-group"">";
            var endCard = "</div>";

            sb.AppendLine(startCard);
            int counter = 1;

            for (int i = 0; i < tubes.Count; i++)
            {
                var tube = tubes[i];
                var tubeId = string.Join("", tube.YoutubeId.Reverse().Take(11));

                var result = $@"<div class=""card col-4 text-center""> 
                <iframe width=""460"" height=""200"" src=""https://www.youtube.com/embed/{tubeId}"" frameborder=""0""
                allowfullscreen></iframe>
                <div class=""card-body"">
                <p class=""card-text""><strong>Title</strong>: {tube.Title}</p>
                <p class=""card-text""><strong>Author</strong>: {tube.Author}</p>
                </div>
                </div>";

                sb.AppendLine(result);

                if (counter == 3)
                {
                    sb.AppendLine(endCard);
                    sb.AppendLine(startCard);
                    counter = 1;
                    continue;
                }

                counter++;
            }

            return endCard;
        }
    }
}
