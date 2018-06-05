using HTTPServer.ByTheCakeApplication.Data.Configurations;
using HTTPServer.ByTheCakeApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace HTTPServer.ByTheCakeApplication.Data
{
    public class ByTheCakeDbContext : DbContext
    {
        private const string ConnectionString = @"Server=.;Database=ByTheCake;Integrated Security=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(ConnectionString);
        }


        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductOrder> ProductsOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());

            modelBuilder.ApplyConfiguration(new ProductOrderConfig());
        }
    }
}
