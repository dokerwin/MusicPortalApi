using Microsoft.EntityFrameworkCore;
using RestaurantAPI.RestaurantRoot.Entities;

namespace RestaurantAPI
{
    public class RestaurantDbContextcs : DbContext
    {
        private string _connectioString = "Server=WINDOWS-ALLPMAQ;Database=RestaurantDb;Trusted_Connection=True;";
        public DbSet<Restaurant> Restaurants { get; set;}
        public DbSet<Address> Address { get; set;}
        public DbSet<Dish> Dishes { get; set;}
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Dish>()
                .Property(r => r.Name)
                .HasMaxLength(50)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectioString);
        }
    }
}
