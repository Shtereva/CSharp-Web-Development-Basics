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
                return db.Users.Single(u => u.Email == email ).IsAdmin;
            }
        }
    }
}
