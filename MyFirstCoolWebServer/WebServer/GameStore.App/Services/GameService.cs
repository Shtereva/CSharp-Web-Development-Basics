namespace HTTPServer.GameStore.App.Services
{
    using ViewModels;
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

        public IEnumerable<AllGamesViewModel> List(string user, string filter)
        {
            using (var db = new GamestoreAppDbContext())
            {
                var orderedGames = db.Games
                    .OrderBy(g => g.Id);

                Game[] games = null;

                if (filter == "Owned")
                {
                    games = orderedGames
                        .Where(g => g.Users.Any(u => u.User.Email == user))
                        .ToArray();
                }
                else
                {
                    games = orderedGames.ToArray();
                }

                return games
                    .Select(g => new AllGamesViewModel()
                    {
                        Id = g.Id.ToString(),
                        ImageTumbnail = g.ImageTumbnail,
                        Title = g.Title,
                        Price = g.Price,
                        Size = g.Size,
                        Description = g.Description
                    })
                    .ToArray();
            }
        }

        public AddGameViewModel Find(int id)
        {
            using (var db = new GamestoreAppDbContext())
            {
                var game = db.Games.Find(id);

                if (game == null)
                {
                    return null;
                }


                return new AddGameViewModel()
                {
                    Price = game.Price.ToString("F2"),
                    Description = game.Description,
                    TrailerId = game.TrailerId,
                    ImageTumbnail = game.ImageTumbnail,
                    Size = game.Size.ToString("F1"),
                    Title = game.Title,
                    ReleaseDate = game.ReleaseDate.ToString("yyyy-MM-dd")
                };
            }
        }

        public IList<CartViewModel> GetCart(List<int> ids)
        {
            using (var db = new GamestoreAppDbContext())
            {
                return db.Games
                    .Where(g => ids.Contains(g.Id))
                    .Select(g => new CartViewModel()
                    {
                        Description = g.Description,
                        GameId = g.Id,
                        Price = g.Price,
                        Title = g.Title,
                        Tumbnail = g.ImageTumbnail
                    })
                    .ToList();
            }
        }

        public bool Edit(int id, AddGameViewModel viewModel)
        {
            try
            {
                using (var db = new GamestoreAppDbContext())
                {
                    var game = db.Games.Find(id);

                    if (game == null)
                    {
                        return false;
                    }

                    game.Price = decimal.Parse(viewModel.Price);
                    game.Description = viewModel.Description;
                    game.ImageTumbnail = viewModel.ImageTumbnail;
                    game.ReleaseDate = DateTime.Parse(viewModel.ReleaseDate);
                    game.Size = double.Parse(viewModel.Size);
                    game.Title = viewModel.Title;
                    game.TrailerId = viewModel.TrailerId;

                    db.Update(game);
                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (var db = new GamestoreAppDbContext())
                {
                    var game = db.Games.Find(id);

                    if (game == null)
                    {
                        return false;
                    }

                    db.Remove(game);
                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Exist(int id)
        {
            using (var db = new GamestoreAppDbContext())
            {
                return db.Games.Any(g => g.Id == id);
            }
        }
    }
}
