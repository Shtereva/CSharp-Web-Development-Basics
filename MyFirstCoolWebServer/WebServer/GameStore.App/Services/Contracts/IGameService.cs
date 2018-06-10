using System;

namespace HTTPServer.GameStore.App.Services.Contracts
{
    public interface IGameService
    {
        bool Create(string title, string description, string thumbnail, decimal price, double size, string trailerId,
            DateTime releaseDate);
    }
}
