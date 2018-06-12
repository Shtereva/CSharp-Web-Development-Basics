namespace HTTPServer.GameStore.App.Services.Contracts
{
    using ViewModels.Home;
    using System;
    using System.Collections.Generic;
    using ViewModels.Admin;
    public interface IGameService
    {
        bool Create(string title, string description, string thumbnail, decimal price, double size, string trailerId,
            DateTime releaseDate);

        IEnumerable<AdminListGamesViewModel> All();

        IEnumerable<AllGamesViewModel> List();
    }
}
