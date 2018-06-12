namespace HTTPServer.GameStore.App.Services
{
    using ViewModels.Home;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Models;
    using ViewModels.Admin;
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

        public IEnumerable<AdminListGamesViewModel> All()
        {
            using (var db = new GamestoreAppDbContext())
            {
                return db.Games
                    .OrderBy(g => g.Id)
                    .Select(g => new AdminListGamesViewModel()
                    {
                        Id = g.Id,
                        Name = g.Title,
                        Price = g.Price,
                        Size = g.Size
                    })
                    .ToArray();
            }
        }

        public IEnumerable<AllGamesViewModel> List()
        {
            using (var db = new GamestoreAppDbContext())
            {
                return db.Games
                    .OrderBy(g => g.Id)
                    .Select(g => new AllGamesViewModel()
                    {
                        ImageTumbnail = g.ImageTumbnail,
                        Title = g.Title,
                        Price = g.Price,
                        Size = g.Size,
                        Description = g.Description
                    })
                    .ToArray();
            }
        }
    }
}
