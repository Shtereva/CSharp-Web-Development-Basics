using HTTPServer.ByTheCakeApplication.Models;
using HTTPServer.ByTheCakeApplication.ViewModels.Account;

namespace HTTPServer.ByTheCakeApplication.Services.Contracts
{
    public interface IUserService
    {
        bool Create(string username, string password);

        bool FindByUsername(string username, string password);

        ProfileViewModel Profile(string username);

        int ById(string username);
    }
}
