namespace Kittens.App.Controllers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Kittens.Models;
    using Models;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Attributes.Security;
    using SimpleMvc.Framework.Interfaces;

    public class KittensController : BaseController
    {
        [HttpGet]
        [PreAuthorize]
        public IActionResult All()
        {
            var sb = new StringBuilder();

            List<AddKittenModel> kittens = null;

            using (this.Context)
            {
                kittens = this.Context.Kittens
                    .Select(k => new AddKittenModel()
                    {
                        Name = k.Name,
                        Age = k.Age,
                        Breed = k.Breed.Type
                    })
                    .ToList();
            }

            string endCard = this.CreateHtml(sb, kittens);

            sb.AppendLine(endCard);

            this.Model.Data["kittens"] = sb.ToString();
            return this.View();
        }

        [HttpGet]
        [PreAuthorize]
        public IActionResult Add()
        {
            this.Model.Data["error"] = string.Empty;
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddKittenModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToAction("/");
            }

            if (!this.IsValidModel(model))
            {
                this.Model.Data["error"] = "Invalid kitten data";
            }

            using (this.Context)
            {
                var breed = new Breed() { Type = model.Breed };
                var kitten = new Kitten()
                {
                    Age = model.Age,
                    Breed = breed,
                    Name = model.Name
                };

                this.Context.Add(kitten);
                this.Context.SaveChanges();
            }

            return this.RedirectToAction("/kittens/all");
        }

        private string CreateHtml(StringBuilder sb, List<AddKittenModel> kittens)
        {
            var imgNames = new Dictionary<string, string>()
            {
                ["Street Transcended"] = "street-transcended",
                ["American Shorthair"] = "american-shorthair",
                ["Munchkin"] = "munchkin",
                ["Siamese"] = "siamese"
            };

            var startCard = $@"<div class=""card-group"">";
            var endCard = "</div>";

            sb.AppendLine(startCard);
            int counter = 1;

            for (int i = 0; i < kittens.Count; i++)
            {
                var kitten = kittens[i];
                
                var imageName = imgNames[kitten.Breed];

                var image = $@"/Content/img/{imageName}.jpg";

                var result = $@"<div class=""card col-4 thumbnail""> 
                <img class=""card-image-top img-fluid img-thumbnail"" onerror=""this.src='{image}';"" src=""{image}"">
                <div class=""card-body"">
                <p class=""card-text""><strong>Name</strong>: {kitten.Name}</p>
                <p class=""card-text""><strong>Age</strong>: {kitten.Age}</p>
                <p class=""card-text""><strong>Breed</strong>: {kitten.Breed}</p>
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
