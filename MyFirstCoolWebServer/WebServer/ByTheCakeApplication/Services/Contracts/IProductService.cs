using System.Collections.Generic;
using HTTPServer.ByTheCakeApplication.ViewModels;

namespace HTTPServer.ByTheCakeApplication.Services.Contracts
{
    public interface IProductService
    {
        bool Add(ProductViewModel cake);

        IEnumerable<ProductListingViewModel> All(string searchTerm = null);

        ProductViewModel FindById(int id);

        bool Exists(int id);

        IList<ShoppingCartViewModel> GetShoppingCart(IEnumerable<int> productIds);
    }
}
