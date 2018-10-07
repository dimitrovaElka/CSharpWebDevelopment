namespace CakesWebApp.Data
{
    using CakesWebApp.Models;
    using Microsoft.EntityFrameworkCore;

    public class CakesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=.;Database=Cakes;Integrated Security=True");
        }
    }
}
