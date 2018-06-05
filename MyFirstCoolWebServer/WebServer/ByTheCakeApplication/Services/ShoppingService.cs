using System.Collections.Generic;
using System.Linq;
using HTTPServer.ByTheCakeApplication.Models;
using HTTPServer.ByTheCakeApplication.ViewModels;
using HTTPServer.Server.Http.Response;

namespace HTTPServer.ByTheCakeApplication.Services
{
    using Contracts;
    using Data;
    using System;
    public class ShoppingService : IShoppingService
    {
        public void CreateOrder(int userId, IEnumerable<int> productIds)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var order = new Order()
                {
                    UserId = userId,
                    CreatedOn = DateTime.UtcNow,
                    Products = productIds.Select(id => new ProductOrder()
                    {
                        ProductId = id
                    })
                                .ToList()
                };

                db.Orders.Add(order);
                db.SaveChanges();
            }
        }

        public IEnumerable<OrderViewModel> GetOrders(int userId)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var user = db.Users.Find(userId);

                if (user == null)
                {
                    throw new InvalidCastException("User does not exist!");
                }

                return db.Orders
                    .Where(o => o.UserId == userId)
                    .OrderByDescending(o => o.CreatedOn)
                    .Select(o => new OrderViewModel()
                    {
                        OrderId = o.Id,
                        CreatedOn = o.CreatedOn,
                        Sum = o.Products.Sum(p => p.Product.Price)
                    })
                    .ToList();
            }
        }

        public IEnumerable<ProductListingViewModel> Find(int id)
        {
            using (var db = new ByTheCakeDbContext())
            {
                if (!db.Orders.Any(o => o.Id == id))
                {
                    throw new  InvalidOperationException("No such order!");
                }

                var product =  db.ProductsOrders
                    .Where(po => po.OrderId == id)
                    .Select(p => new ProductListingViewModel()
                    {
                        Id = p.ProductId.ToString(),
                        Name = p.Product.Name,
                        Price = p.Product.Price
                    })
                    .ToArray();

                return product;
            }
        }
    }
}
