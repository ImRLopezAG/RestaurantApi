using Restaurant.Core.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Infrastructure.Persistence.Context;

public class RestaurantContext : DbContext
{
  public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) { }

  public DbSet<Ingredient> Ingredients { get; set; }
  public DbSet<Plate> Plates { get; set; }
  public DbSet<PlateIngredient> PlateIngredients { get; set; }
  public DbSet<PlateCategory> PlateCategories { get; set; }
  public DbSet<Table> Tables { get; set; }
  public DbSet<TableStatus> TableStatuses { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<OrderStatus> OrderStatuses { get; set; }

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
  {
    foreach (var entry in ChangeTracker.Entries<BaseEntity>())
      switch (entry.State)
      {
        case EntityState.Added:
          entry.Entity.CreatedAt = DateTime.Now;
          break;
        case EntityState.Modified:
          entry.Entity.LastModifiedAt = DateTime.Now;
          break;
      }
    return base.SaveChangesAsync(cancellationToken);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder){
    #region  Tables
    modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
    modelBuilder.Entity<Plate>().ToTable("Plates");
    modelBuilder.Entity<PlateIngredient>().ToTable("PlateIngredients");
    modelBuilder.Entity<PlateCategory>().ToTable("PlateCategories");
    modelBuilder.Entity<Table>().ToTable("Tables");
    modelBuilder.Entity<TableStatus>().ToTable("TableStatuses");
    modelBuilder.Entity<Order>().ToTable("Orders");
    modelBuilder.Entity<OrderStatus>().ToTable("OrderStatuses");

    #endregion

    #region  Primary Keys
    modelBuilder.Entity<Ingredient>().HasKey(x => x.Id);
    modelBuilder.Entity<Plate>().HasKey(x => x.Id);
    modelBuilder.Entity<PlateIngredient>().HasKey(x => x.Id);
    modelBuilder.Entity<PlateCategory>().HasKey(x => x.Id);
    modelBuilder.Entity<Table>().HasKey(x => x.Id);
    modelBuilder.Entity<TableStatus>().HasKey(x => x.Id);
    modelBuilder.Entity<Order>().HasKey(x => x.Id);
    modelBuilder.Entity<OrderStatus>().HasKey(x => x.Id);

    #endregion

    #region  Relationships
    modelBuilder.Entity<PlateIngredient>()
      .HasOne(x => x.Plate)
      .WithMany(x => x.PlateIngredients)
      .HasForeignKey(x => x.PlateId);

    modelBuilder.Entity<PlateIngredient>()
      .HasOne(x => x.Ingredient)
      .WithMany(x => x.PlateIngredients)
      .HasForeignKey(x => x.IngredientId);

    modelBuilder.Entity<Plate>()
      .HasOne(x => x.PlateCategory)
      .WithMany(x => x.Plates)
      .HasForeignKey(x => x.PlateCategoryId);

    modelBuilder.Entity<Order>()
      .HasOne(x => x.Table)
      .WithMany(x => x.Orders)
      .HasForeignKey(x => x.TableId);

    modelBuilder.Entity<Order>()
      .HasOne(x => x.OrderStatus)
      .WithMany(x => x.Orders)
      .HasForeignKey(x => x.OrderStatusId);

    #endregion

    #region Seeds
    modelBuilder.Entity<TableStatus>().HasData(
      new TableStatus { Id = 1, Name = "Available", CreatedAt = DateTime.Now },
      new TableStatus { Id = 2, Name = "Occupied", CreatedAt = DateTime.Now },
      new TableStatus { Id = 3, Name = "Attendant", CreatedAt = DateTime.Now }
    );

    modelBuilder.Entity<OrderStatus>().HasData(
      new OrderStatus { Id = 1, Name = "In Progress", CreatedAt = DateTime.Now },
      new OrderStatus { Id = 2, Name = "Completed", CreatedAt = DateTime.Now }
    );

    modelBuilder.Entity<PlateCategory>().HasData(
      new PlateCategory { Id = 1, Name = "Entrance", CreatedAt = DateTime.Now },
      new PlateCategory { Id = 2, Name = "Main Course", CreatedAt = DateTime.Now },
      new PlateCategory { Id = 3, Name = "Dessert", CreatedAt = DateTime.Now }
    );

    #endregion
  }
}