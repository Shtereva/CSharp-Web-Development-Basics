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
    using ViewModels;
    using SimpleMvc.Framework.Contracts.Generic;
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
        public IActionResult<AllUsersViewModel> All()
        {
            List<UserViewModel> users = null;

            using (var db = new NotesDbContext())
            {
                users = db.Users
                    .Select(u => new UserViewModel()
                    {
                        UserId = u.Id,
                        Username = u.Username
                    })
                    .ToList();
            }

            var viewModel = new AllUsersViewModel()
            {
                Usernames = users
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult<UserProfileViewModel> Profile(int id)
        {
            string username = null;
            List<NoteViewModel> notes = null;

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

                username = user.Username;
                notes = user.Notes
                    .Select(n => new NoteViewModel()
                    {
                        Title = n.Title,
                        Content = n.Content
                    })
                    .ToList();
            }

            var viewModel = new UserProfileViewModel()
            {
                UserId = id,
                Username = username,
                Notes = notes
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult<UserProfileViewModel> Profile(AddNoteBindingViewModel model)
        {
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
