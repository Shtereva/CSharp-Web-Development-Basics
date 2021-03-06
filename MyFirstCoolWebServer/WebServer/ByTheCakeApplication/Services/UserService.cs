﻿using System;
using System.Linq;
using HTTPServer.ByTheCakeApplication.Data;
using HTTPServer.ByTheCakeApplication.Models;
using HTTPServer.ByTheCakeApplication.Services.Contracts;
using HTTPServer.ByTheCakeApplication.ViewModels.Account;

namespace HTTPServer.ByTheCakeApplication.Services
{
    public class UserService : IUserService
    {
        public bool Create(string username, string password)
        {
            using (var db = new ByTheCakeDbContext())
            {
                if (db.Users.Any(u => u.Username == username))
                {
                    return false;
                }

                var user = new User()
                {
                    Username = username,
                    Password = password,
                    RegisteredOn = DateTime.UtcNow
                };

                db.Users.Add(user);
                db.SaveChanges();

                return true;
            }
        }

        public bool FindByUsername(string username, string password)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Users.Any(u => u.Username == username && u.Password == password);
            }
        }

        public ProfileViewModel Profile(string username)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);

                return new ProfileViewModel()
                {
                    Username = user.Username,
                    RegisteredOn = user.RegisteredOn.ToString(),
                    OrdersCount = user.Orders.Count.ToString()
                };
            }
        }

        public int ById(string username)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new  InvalidOperationException("User doesn't exist!");
                }

                return user.Id;
            }
        }
    }
}
