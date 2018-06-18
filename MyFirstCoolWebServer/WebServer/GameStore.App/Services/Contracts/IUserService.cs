namespace HTTPServer.GameStore.App.Services.Contracts
{
    using System.Collections.Generic;
    using ViewModels.Home;
    public interface IUserService
    {
        bool Create(string email, string fullName, string password);

        bool Find(string email, string password);

        bool IsAdmin(string email);

        bool AddProducts(List<int> ids, string userId);

        bool GameExist(int gameId, string userId);
    }
}
