using System.Collections.Generic;

namespace HTTPServer.GameStore.App.Services.Contracts
{
    using ViewModels.Admin;
    public interface IUserService
    {
        bool Create(string email, string fullName, string password);

        bool Find(string email, string password);

        bool IsAdmin(string email);

        bool AddProducts(List<int> ids, string userId);

        bool GameExist(int gameId, string userId);
    }
}
