namespace HTTPServer.GameStore.App.ViewModels.Home
{
    public class AllGamesViewModel
    {
        public string Id { get; set; }
        public string ImageTumbnail { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        // In GB
        public double Size { get; set; }

        public string Description { get; set; }
    }
}
