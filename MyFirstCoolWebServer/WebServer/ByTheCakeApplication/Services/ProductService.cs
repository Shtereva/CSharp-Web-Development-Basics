namespace HTTPServer.ByTheCakeApplication.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Models;
    using Contracts;
    using ViewModels;
    public class ProductService : IProductService
    {
        public bool Add(ProductViewModel cake)
        {
            using (var db = new ByTheCakeDbContext())
            {
                if (db.Products.Any(p => p.Name == cake.Name))
                {
                    return false;
                }

                var product = new Product()
                {
                    Name = cake.Name,
                    Price = cake.Price,
                    ImageUrl = cake.ImageUrl,
                };

                db.Products.Add(product);
                db.SaveChanges();

                return true;
            }
        }

        public IEnumerable<ProductListingViewModel> All(string searchTerm = null)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var products = db.Products.AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products
                        .Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()));
                }

                return products
                    .Select(p => new ProductListingViewModel()
                    {
                        Id = p.Id.ToString(),
                        Name = p.Name,
                        Price = p.Price
                    })
                    .ToList();
            }
        }

        public ProductViewModel FindById(int id)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var product = db.Products.Find(id);

                return new ProductViewModel()
                {
                    Name = product.Name,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl
                };
            }
        }

        public bool Exists(int id)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Products.Any(p => p.Id == id);
            }
        }

        public IList<ShoppingCartViewModel> GetShoppingCart(IEnumerable<int> productIds)
        {
            return this.All()
                 .Where(p => productIds.Contains(int.Parse(p.Id)))
                 .Select(p => new ShoppingCartViewModel()
                 {
                    Name = p.Name,
                    Price = p.Price
                 })
                .ToList();
        }
    }
}
