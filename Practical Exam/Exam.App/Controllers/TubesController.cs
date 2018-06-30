namespace MeTube.App.Controllers
{
    using System.Linq;
    using MeTube.Models;
    using Models.BindingModels;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Attributes.Security;
    using SimpleMvc.Framework.Interfaces;

    public class TubesController : BaseController
    {
        [HttpGet]
        [PreAuthorize]
        public IActionResult Details(int id)
        {
            using (this.Context)
            {
                var tube = this.Context.Tubes.Find(id);

                if (tube == null)
                {
                    return this.RedirectToAction("/");
                }

                tube.Views++;

                var tubeId = string.Join("", tube.YoutubeId.Reverse().Take(11));

                this.Model.Data["title"] = tube.Title;
                this.Model.Data["tubeId"] = tubeId;
                this.Model.Data["author"] = tube.Author;
                this.Model.Data["views"] = tube.Views.ToString();
                this.Model.Data["description"] = tube.Description;

                this.Context.Update(tube);
                this.Context.SaveChanges();
            }

            return this.View();
        }

        [HttpGet]
        [PreAuthorize]
        public IActionResult Upload()
        {
            this.Model.Data["error"] = string.Empty;
            return this.View();
        }

        [HttpPost]
        [PreAuthorize]
        public IActionResult Upload(UploadTubeBindingModel model)
        {
            if (!this.IsValidModel(model))
            {
                this.Model.Data["error"] = "Invalid video data";
            }

            using (this.Context)
            {
                var user = this.Context.Users.SingleOrDefault(u => u.Username == this.User.Name);

                if (user == null)
                {
                    return this.View();
                }

                var tube = new Tube()
                {
                    Author = model.Author,
                    Title = model.Title,
                    Description = model.Description,
                    YoutubeId = model.YoutubeId,
                    UploaderId = user.Id
                };

                this.Context.Add(tube);
                this.Context.SaveChanges();
            }

            return this.RedirectToAction("/");
        }
    }
}
