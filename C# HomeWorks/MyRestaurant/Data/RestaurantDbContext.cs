
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Data.Configurations;
using MyRestaurant.Entitites;
using System.Reflection;

namespace MyRestaurant.Data;

public class RestaurantDbContext : DbContext
{

    private const string connectionString
        = "Server=localhost;Database=Restaurant;Integrated Security=True;TrustServerCertificate=True;";

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Menu> Menu { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Waiter> Waiters { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}
