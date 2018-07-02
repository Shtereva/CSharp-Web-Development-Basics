namespace Chushka.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Chushka.Models;
    using Models.ViewModels;
    using SoftUni.WebServer.Mvc.Attributes.HttpMethods;
    using SoftUni.WebServer.Mvc.Attributes.Security;
    using SoftUni.WebServer.Mvc.Interfaces;

    public class OrdersController : BaseController
    {
        [HttpGet]
        [Authorize]
        public IActionResult All()
        {
            if (!this.User.IsInRole("1"))
            {
                return this.RedirectToAction("/");
            }

            List<string> ordersResult = null;

            using (this.Context)
            {

                var orders = this.Context.Orders
                    .Select(o => new OrdersViewModel()
                    {
                        Id = o.Id.ToString(),
                        Customer = o.Client.Username,
                        Product = o.Product.Name,
                        OrderedOn = o.OrderedOn.ToShortDateString()
                    })
                    .ToArray();

                int num = 1;

                ordersResult = orders
                   .Select(o => $@"<tr>
                <th scope=""row"">{num++}</th>
                <td>{o.Id}</td>
                <td>{o.Customer}</td>
                <td>{o.Product}</td>
                <td>{o.OrderedOn}</td>
                </td>
                </tr>")
                   .ToList();
            }

            this.ViewData["orders"] = string.Join(Environment.NewLine, ordersResult);

            return this.View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Order(int id)
        {
            if (!this.User.IsInRole("1"))
            {
                return this.RedirectToAction("/");
            }

            var order = new Order()
            {
                ClientId = this.User.Id,
                ProductId = id,
                OrderedOn = DateTime.UtcNow
            };

            this.Context.Add(order);
            this.Context.SaveChanges();

            return this.RedirectToAction("/");
        }
    }
}
