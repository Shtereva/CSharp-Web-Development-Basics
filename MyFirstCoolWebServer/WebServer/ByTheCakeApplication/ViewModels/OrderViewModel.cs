using System;

namespace HTTPServer.ByTheCakeApplication.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public decimal Sum { get; set; }
    }
}
