namespace HTTPServer.GameStore.App.Services.Contracts
{
    using ViewModels;
    using ViewModels.Home;
    using System;
    using System.Collections.Generic;
    using ViewModels.Admin;
    public interface IGameService
    {
        bool Create(string title, string description, string thumbnail, decimal price, double size, string trailerId,
            DateTime releaseDate);

        IEnumerable<AdminListGamesViewModel> All();

        IEnumerable<AllGamesViewModel> List(string user, string filter);

        AddGameViewModel Find(int id);

        bool Edit(int id, AddGameViewModel viewModel);

        bool Delete(int id);

        bool Exist(int id);

        IList<CartViewModel> GetCart(List<int> ids);
    }
}
