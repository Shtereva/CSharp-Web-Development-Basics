using System;
using System.Collections.Generic;
using HTTPServer.GameStore.App.ViewModels.Admin;

namespace HTTPServer.GameStore.App.Services
{
    using Data;
    using Contracts;
    using System.Linq;
    using Models;

    public class UserService : IUserService
    {
        public bool Create(string email, string fullName, string password)
        {
            using (var db = new GamestoreAppDbContext())
            {
                if (db.Users.Any(u => u.Email == email))
                {
                    return false;
                }

                var user = new User()
                {
                    FullName = fullName,
                    Email = email,
                    Password = password,
                    IsAdmin = !db.Users.Any()
                };

                db.Users.Add(user);
                db.SaveChanges();

                return true;
            }
        }

        public bool Find(string email, string password)
        {
            using (var db = new GamestoreAppDbContext())
            {
                return db.Users.Any(u => u.Email == email && u.Password == password);
            }
        }

        public bool IsAdmin(string email)
        {
            using (var db = new GamestoreAppDbContext())
            {
                return db.Users.Single(u => u.Email == email).IsAdmin;
            }
        }

        public bool AddProducts(List<int> ids, string userId)
        {
            try
            {
                using (var db = new GamestoreAppDbContext())
                {
                    var games = db.Games
                        .Where(g => ids.Contains(g.Id))
                        .ToList();

                    var user = db.Users.SingleOrDefault(u => u.Email == userId);
                    foreach (var game in games)
                    {
                        user.Games.Add(new UserGame()
                        {
                            GameId = game.Id
                        });
                    }

                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool GameExist(int gameId, string userId)
        {
            try
            {
                using (var db = new GamestoreAppDbContext())
                {
                    var user = db.Users.SingleOrDefault(u => u.Email == userId);

                    var exist  = db.UserGame.Any(ug => ug.GameId == gameId && ug.UserId == user.Id);
                    return exist;
                }
            }
            catch 
            {
                return false;
            }
        }
    }
}
