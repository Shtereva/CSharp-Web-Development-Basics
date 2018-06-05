using System.Collections.Generic;
using HTTPServer.ByTheCakeApplication.ViewModels;

namespace HTTPServer.ByTheCakeApplication.Services.Contracts
{
    public interface IShoppingService
    {
        void CreateOrder(int userId, IEnumerable<int> productIds);

        IEnumerable<OrderViewModel> GetOrders(int userId);

        IEnumerable<ProductListingViewModel> Find(int id);
    }
}
