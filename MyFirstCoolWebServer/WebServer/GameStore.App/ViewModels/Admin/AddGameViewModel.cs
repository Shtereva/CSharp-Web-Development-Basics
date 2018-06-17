namespace HTTPServer.GameStore.App.ViewModels.Admin
{
    using System;
    public class AddGameViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageTumbnail { get; set; }


        public string Price { get; set; }

        // In GB
        public string Size { get; set; }

        public string TrailerId { get; set; }

        public string ReleaseDate { get; set; }
    }
}
