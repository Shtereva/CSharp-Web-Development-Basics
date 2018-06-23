namespace Notes.App.ViewModels
{
    using System.Collections.Generic;
    using Models;
    public class UserProfileViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public List<NoteViewModel> Notes { get; set; }
    }
}
