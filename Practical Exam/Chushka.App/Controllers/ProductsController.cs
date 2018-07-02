namespace Chushka.App.Controllers
{
    using System.Linq;
    using Chushka.Models;
    using Models.BindingModels;
    using Models.ViewModels;
    using SoftUni.WebServer.Mvc.Attributes.HttpMethods;
    using SoftUni.WebServer.Mvc.Attributes.Security;
    using SoftUni.WebServer.Mvc.Interfaces;


    public class ProductsController : BaseController
    {
        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            if (!this.User.IsInRole("1"))
            {
                return this.RedirectToAction("/");
            }

            using (this.Context)
            {
                var product = this.Context.Products
                    .Where(p => p.Id == id)
                    .Select(p => new CreateProductBindingModel()
                    {
                        Id = id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        FoodTypeId = p.FoodTypeId,
                    })
                    .SingleOrDefault();


                if (product == null)
                {
                    return this.RedirectToAction("/");
                }

                this.ViewData["description"] = product.Description;
                this.ViewData["price"] = product.Price.ToString("F2");
                this.ViewData["name"] = product.Name;
                this.ViewData["error"] = string.Empty;
                this.ViewData["id"] = id.ToString();
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(CreateProductBindingModel model)
        {
            if (!this.User.IsInRole("1"))
            {
                return this.RedirectToAction("/");
            }

            if (!this.IsValidModel(model))
            {
                this.ViewData["error"] = "Invalid product data";
                return this.View();
            }

            using (this.Context)
            {
                var product = this.Context.Products.Find(model.Id);

                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.FoodTypeId = model.FoodTypeId;

                this.Context.Update(product);
                this.Context.SaveChanges();
            }

            return this.RedirectToAction("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!this.User.IsInRole("1"))
            {
                return this.RedirectToAction("/");
            }

            using (this.Context)
            {
                var product = this.Context.Products
                    .Where(p => p.Id == id)
                    .Select(p => new CreateProductBindingModel()
                    {
                        Id = id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        FoodTypeId = p.FoodTypeId,
                    })
                    .SingleOrDefault();


                if (product == null)
                {
                    return this.RedirectToAction("/");
                }

                this.ViewData["description"] = product.Description;
                this.ViewData["price"] = product.Price.ToString("F2");
                this.ViewData["name"] = product.Name;
                this.ViewData["error"] = string.Empty;
                this.ViewData["id"] = id.ToString();
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(CreateProductBindingModel model)
        {
            if (!this.User.IsInRole("1"))
            {
                return this.RedirectToAction("/");
            }

            using (this.Context)
            {
                var product = this.Context.Products.Find(model.Id);

                if (product == null)
                {
                    return this.RedirectToAction("/");
                }

                product.IsDeleted = true;
                this.Context.SaveChanges();
            }

            return this.RedirectToAction("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            if (!this.User.IsInRole("1"))
            {
                return this.RedirectToAction("/");
            }

            this.ViewData["error"] = string.Empty;
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateProductBindingModel model)
        {
            if (!this.User.IsInRole("1") || model.FoodTypeId <= 0 || model.FoodTypeId > 5)
            {
                return this.RedirectToAction("/");
            }

            if (!this.IsValidModel(model))
            {
                this.ViewData["error"] = "Invalid product data";
                return this.View();
            }

            using (this.Context)
            {
                var product = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    FoodTypeId = model.FoodTypeId,
                };

                this.Context.Add(product);
                this.Context.SaveChanges();
            }

            return this.RedirectToAction("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(int id)
        {
            var product = this.Context.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailsViewModel()
                {
                    Name = p.Name,
                    Description = p.Description,
                    FoodType = p.FoodType.Name,
                    Price = p.Price,
                    IsDeleted = p.IsDeleted
                })
                .FirstOrDefault();

            if (product == null || product.IsDeleted)
            {
                return this.RedirectToAction("/");
            }

            this.ViewData["adminDisplay"] = this.User.IsInRole("1") ? "block" : "none";

            this.ViewData["name"] = product.Name;
            this.ViewData["description"] = product.Description;
            this.ViewData["foodType"] = product.FoodType;
            this.ViewData["price"] = product.Price.ToString();
            this.ViewData["id"] = id.ToString();

            return this.View();
        }
    }
}
