namespace Kittens.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class KittenDbContext : DbContext
    {
        private const string ConnectionString = @"Server=.;Database=FDMC;Integrated Security=True";

        public DbSet<User> Users { get; set; }

        public DbSet<Kitten> Kittens { get; set; }

        public DbSet<Breed> Breeds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kitten>()
                .HasOne(k => k.Breed)
                .WithMany(b => b.Kittens)
                .HasForeignKey(k => k.BreedId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
