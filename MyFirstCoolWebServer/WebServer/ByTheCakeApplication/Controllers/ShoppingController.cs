namespace HTTPServer.ByTheCakeApplication.Controllers
{
    using System;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System.Linq;
    using ViewModels;
    using Services;
    using Services.Contracts;
    public class ShoppingController : BaseController
    {
        private readonly IShoppingService shoppingService;
        private readonly IProductService productService;
        private readonly IUserService userService;

        public ShoppingController()
        {
            this.shoppingService = new ShoppingService();
            this.productService = new ProductService();
            this.userService = new UserService();
        }

        public IHttpResponse AddToCart(IHttpRequest req)
        {
            var id = int.Parse(req.UrlParameters["id"]);

            if (!this.productService.Exists(id))
            {
                return new NotFoundResponse();
            }

            var product = this.productService.FindById(id);

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            shoppingCart.Products.Add(id);

            var redirectUrl = "/search";

            const string searchTermKey = "searchTerm";

            if (req.UrlParameters.ContainsKey(searchTermKey)
                && !string.IsNullOrWhiteSpace(req.UrlParameters[searchTermKey]))
            {
                redirectUrl = $"{redirectUrl}?{searchTermKey}={req.UrlParameters[searchTermKey]}";
            }
            
            return new RedirectResponse(redirectUrl);
        }

        public IHttpResponse ShowCart(IHttpRequest req)
        {
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (!shoppingCart.Products.Any())
            {
                this.ViewData["cartItems"] = "No items in your cart";
                this.ViewData["totalCost"] = "0.00";
            }
            else
            {
                var products = this.productService.GetShoppingCart(shoppingCart.Products);

                var items = products
                    .Select(pr => $"<div>{pr.Name} - ${pr.Price:F2}</div><br />");

                var totalPrice = products
                    .Sum(i => i.Price);

                this.ViewData["cartItems"] = string.Join(string.Empty, items);
                this.ViewData["totalCost"] = $"{totalPrice:F2}";
            }

            return this.FileViewResponse(@"shopping\cart");
        }

        public IHttpResponse FinishOrder(IHttpRequest req)
        {
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            var productIds = shoppingCart.Products;

            var userId = this.userService.ById(req.Session.Get<string>(SessionStore.CurrentUserKey));

            this.shoppingService.CreateOrder(userId, shoppingCart.Products);
            shoppingCart.Products.Clear();

            return this.FileViewResponse(@"shopping\finish-order");
        }

        public IHttpResponse ListOrders(IHttpRequest req)
        {
            var userId = this.userService.ById(req.Session.Get<string>(SessionStore.CurrentUserKey));

            var result = this.shoppingService.GetOrders(userId)
                .Select(o => $@"<tr><td><a href=""/orderdetails/{o.OrderId}"">{o.OrderId}</a></td><td>{o.CreatedOn}</td><td>${o.Sum:f2}</td></tr>");

            var textView = string.Join(Environment.NewLine, result);

            this.ViewData["results"] = textView;

            return this.FileViewResponse(@"shopping/orders");
        }

        public IHttpResponse OrderDetails(int id)
        {
            var orders = this.shoppingService.Find(id);

            if (orders == null)
            {
                return new NotFoundResponse();
            }

            var result = orders
                .Select(o => $@"<tr><td><a href=""/products/{o.Id}"">{o.Name}</a></td><td>${o.Price:f2}</td></tr>");



            this.ViewData["orderid"] = id.ToString();
            this.ViewData["results"] = string.Join(Environment.NewLine, result);

            return this.FileViewResponse($@"shopping/orderdetails");
        }
    }
}
