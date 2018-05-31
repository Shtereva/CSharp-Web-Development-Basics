using System.Linq;
using HTTPServer.ByTheCakeApplication.Models;
using MyFirstCoolWebServer.ByTheCakeApplication.Data;
using MyFirstCoolWebServer.ByTheCakeApplication.Infrastructure;
using MyFirstCoolWebServer.Server.HTTP.Contracts;
using MyFirstCoolWebServer.Server.HTTP.Response;

namespace MyFirstCoolWebServer.ByTheCakeApplication.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly CakesData cakesData;

        public ShoppingController()
        {
            this.cakesData = new CakesData();
        }

        public IHttpResponse AddToCart(IHttpRequest req)
        {
            var id = int.Parse(req.UrlParameters["id"]);

            var cake = this.cakesData.Find(id);

            if (cake == null)
            {
                return new NotFoundResponse();
            }

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            shoppingCart.Orders.Add(cake);

            var redirectUrl = "/search";

            const string searchTermKey = "searchTerm";

            if (req.UrlParameters.ContainsKey(searchTermKey))
            {
                redirectUrl = $"{redirectUrl}?{searchTermKey}={req.UrlParameters[searchTermKey]}";
            }

            return new RedirectResponse(redirectUrl);
        }

        public IHttpResponse ShowCart(IHttpRequest req)
        {
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (!shoppingCart.Orders.Any())
            {
                this.ViewData["cartItems"] = "No items in your cart";
                this.ViewData["totalCost"] = "0.00";
            }
            else
            {
                var items = shoppingCart
                    .Orders
                    .Select(i => $"<div>{i.Name} - ${i.Price:F2}</div><br />");

                var totalPrice = shoppingCart
                    .Orders
                    .Sum(i => i.Price);

                this.ViewData["cartItems"] = string.Join(string.Empty, items);
                this.ViewData["totalCost"] = $"{totalPrice:F2}";
            }

            return this.FileViewResponse(@"shopping\cart");
        }

        public IHttpResponse FinishOrder(IHttpRequest req)
        {
            req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey).Orders.Clear();

            return this.FileViewResponse(@"shopping\finish-order");
        }
    }
}
