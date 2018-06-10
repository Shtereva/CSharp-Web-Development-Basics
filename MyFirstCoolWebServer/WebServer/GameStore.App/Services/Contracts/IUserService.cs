namespace HTTPServer.GameStore.App.Services.Contracts
{
    public interface IUserService
    {
        bool Create(string email, string fullName, string password);

        bool Find(string email, string password);

        bool IsAdmin(string email);
    }
}
