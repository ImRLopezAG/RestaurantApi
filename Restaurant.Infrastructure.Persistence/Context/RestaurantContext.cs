using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Domain.Core;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Infrastructure.Persistence.Context;

public class RestaurantContext : DbContext {
  public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) { }

  public DbSet<Ingredient> Ingredients { get; set; }
  public DbSet<Plate> Plates { get; set; }
  public DbSet<PlateIngredient> PlateIngredients { get; set; }
  public DbSet<Table> Tables { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<OrderPlate> OrderPlates { get; set; }

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new()) {
    foreach (var entry in ChangeTracker.Entries<BaseEntity>())
      switch (entry.State) {
        case EntityState.Added:
          entry.Entity.CreatedAt = DateTime.Now;
          break;
        case EntityState.Modified:
          entry.Entity.LastModifiedAt = DateTime.Now;
          break;
      }
    return base.SaveChangesAsync(cancellationToken);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    #region  Tables
    modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
    modelBuilder.Entity<Plate>().ToTable("Plates");
    modelBuilder.Entity<PlateIngredient>().ToTable("PlateIngredients");
    modelBuilder.Entity<Table>().ToTable("Tables");
    modelBuilder.Entity<Order>().ToTable("Orders");
    modelBuilder.Entity<OrderPlate>().ToTable("OrderPlates");

    #endregion

    #region  Primary Keys
    modelBuilder.Entity<Ingredient>().HasKey(x => x.Id);
    modelBuilder.Entity<Plate>().HasKey(x => x.Id);
    modelBuilder.Entity<PlateIngredient>().HasKey(x => x.Id);
    modelBuilder.Entity<Table>().HasKey(x => x.Id);
    modelBuilder.Entity<Order>().HasKey(x => x.Id);

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

    modelBuilder.Entity<Order>()
      .HasOne(x => x.Table)
      .WithMany(x => x.Orders)
      .HasForeignKey(x => x.TableId);

    modelBuilder.Entity<OrderPlate>()
      .HasOne(x => x.Plate)
      .WithMany(x => x.OrderPlates)
      .HasForeignKey(x => x.PlateId);

    modelBuilder.Entity<OrderPlate>()
      .HasOne(x => x.Order)
      .WithMany(x => x.OrderPlates)
      .HasForeignKey(x => x.OrderId);

    #endregion

  }
}