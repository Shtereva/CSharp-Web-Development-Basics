namespace Notes.App.Controllers
{
    using System;
    using System.Linq;
    using Models;
    using BindingModels;
    using Data;
    using SimpleMvc.Framework.Contracts;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Attributes.Methods;
    using System.Collections.Generic;
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterBindingModel model)
        {
            if (!this.IsValidModel(model))
            {
                return this.View();
            }
            using (var db = new NotesDbContext())
            {
                if (db.Users.Any(u => u.Username == model.Username))
                {
                    throw new ArgumentException("Username already taken!");
                }

                var user = new User()
                {
                    Username = model.Username,
                    Password = model.Password
                };

                db.Add(user);
                db.SaveChanges();
            }

            return this.View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginBindingModel model)
        {
            if (!this.IsValidModel(model))
            {
                return this.View();
            }

            using (var db = new NotesDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Username == model.Username && u.Password == model.Password);

                if (user == null)
                {
                    return this.RedirectToAction("/home/login");
                }

                this.SignIn(user.Username);

                return this.RedirectToAction("/home/index");
            }
        }

        [HttpGet]
        public IActionResult All()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToAction("/home/login");
            }

            var users = new Dictionary<int, string>();

            using (var db = new NotesDbContext())
            {
                users = db.Users.ToDictionary(u => u.Id, u => u.Username);
            }

            this.Model["users"] = users.Any()
                ? string.Join(string.Empty, users.Select(u => $@"<li><a href=""/user/profile?id=""{u.Key}"">{u.Value}</a></li>"))
                : string.Empty;

            return this.View();
        }

        [HttpGet]
        public IActionResult Profile(int id)
        {
            string username = null;

            using (var db = new NotesDbContext())
            {
                var user = db.Users
                    .Where(u => u.Id == id)
                    .Select(u => new
                    {
                        u.Username,
                        Notes = u.Notes.ToList()
                    })
                    .SingleOrDefault();

                if (user == null)
                {
                    throw new ArgumentException("User doesn't exist!");
                }

                this.Model["username"] = user.Username;
                this.Model["userid"] = id.ToString();

                var notes = user.Notes.ToList();


                this.Model["users"] = notes.Any()
                    ? string.Join(string.Empty,
                        notes.Select(n => $@"<li>{n.Title} - {n.Content}</li>"))
                    : string.Empty;

                return this.View();
            }
        }

        [HttpPost]
        public IActionResult Profile(AddNoteBindingViewModel model)
        {
            if (!this.IsValidModel(model))
            {
                return this.RedirectToAction($"/user/profile?id={model.UserId}");
            }
            using (var db = new NotesDbContext())
            {
                var user = db.Users.Find(model.UserId);

                user.Notes.Add(new Note()
                {
                    Title = model.Title,
                    Content = model.Content
                });

                db.Update(user);
                db.SaveChanges();
            }

            return this.Profile(model.UserId);
        }
    }
}
