namespace HTTPServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using ViewModels;
    using Server.Http.Contracts;
    using Services;
    using Services.Contracts;
    using System.Linq;
    using System;
    using Server.Http.Response;

    public class ProductController : Controller
    {
        private const string PathView = @"product\add";
        private readonly IProductService productService;

        public ProductController()
        {
            this.productService = new ProductService();
        }

        public IHttpResponse Add()
        {
            this.ViewData["showResult"] = "none";

            return this.FileViewResponse(PathView);
        }

        public IHttpResponse Add(ProductViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.Name)
                || string.IsNullOrWhiteSpace(viewModel.ImageUrl))
            {
                this.AddViewError("You have empty fields");

                return this.FileViewResponse(PathView);
            }

            var success = this.productService.Add(viewModel);

            if (!success)
            {

            }

            this.ViewData["name"] = viewModel.Name;
            this.ViewData["price"] = viewModel.Price.ToString();
            this.ViewData["image-url"] = viewModel.ImageUrl;
            this.ViewData["showResult"] = "block";

            return this.FileViewResponse(PathView);
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            const string searchTermKey = "searchTerm";

            var urlParameters = req.UrlParameters;

            this.ViewData["results"] = string.Empty;
            this.ViewData["searchTerm"] = string.Empty;

            var searchTerm = urlParameters.ContainsKey(searchTermKey) 
                ? urlParameters[searchTermKey] 
                : null;

            this.ViewData["searchTerm"] = searchTerm;

            var result = this.productService.All(searchTerm);

            if (!result.Any())
            {
                this.ViewData["results"] = "Cake Not Found";
            }

            else
            {
                var products = result
                    .Select(c =>
                        $@"<div><a href=""/products/{c.Id}"">{c.Name}</a> - ${c.Price:f2} <a href=""/shopping/add/{c.Id}?searchTerm={searchTerm}"">Order</a></div>");

                this.ViewData["results"] = string.Join(Environment.NewLine, products);
            }


            this.ViewData["showCart"] = "none";

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (shoppingCart.Products.Any())
            {
                var totalProducts = shoppingCart.Products.Count;
                var totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{totalProducts} {totalProductsText}";
            }

            return this.FileViewResponse(@"product/search");
        }

        public IHttpResponse Details(int id)
        {
            var product = this.productService.FindById(id);

            if (product == null)
            {
                return new NotFoundResponse();
            }

            this.ViewData["name"] = product.Name;
            this.ViewData["price"] = product.Price.ToString("F2");
            this.ViewData["image-url"] = product.ImageUrl;

            return this.FileViewResponse($@"product/details");
        }
    }
}
