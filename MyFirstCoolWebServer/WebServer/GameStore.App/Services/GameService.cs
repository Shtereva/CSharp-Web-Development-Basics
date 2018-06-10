using System.Linq;
using HTTPServer.GameStore.App.Data;
using HTTPServer.GameStore.App.Models;

namespace HTTPServer.GameStore.App.Services
{
    using System;
    using Contracts;
    public class GameService : IGameService
    {
        public bool Create(string title, string description, string thumbnail, decimal price, double size, string trailerId,
            DateTime releaseDate)
        {
            using (var db = new GamestoreAppDbContext())
            {
                if (db.Games.Any(g => g.TrailerId == trailerId))
                {
                    return false;
                }
                var game = new Game()
                {
                    Title = title,
                    Description = description,
                    ImageTumbnail = thumbnail,
                    Price = price,
                    Size = size,
                    TrailerId = trailerId,
                    ReleaseDate = releaseDate
                };

                db.Games.Add(game);
                db.SaveChanges();

                return true;
            }
        }
    }
}
