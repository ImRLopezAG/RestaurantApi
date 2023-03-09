using Restaurant.Core.Application.Core;
using Restaurant.Infrastructure.Persistence.Context;
using Restaurant.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Infrastructure.Persistence.Repositories;

namespace Restaurant.Infrastructure.Persistence
{
  public static class ServiceRegistration
  {
    public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
      #region DbContext
      if (configuration.GetValue<bool>("UseInMemoryDatabase"))
      {
        services.AddDbContext<RestaurantContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
      }
      else
      {
        services.AddDbContext<RestaurantContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        m => m.MigrationsAssembly(typeof(RestaurantContext).Assembly.FullName)));
      }
      #endregion
      #region Repositories
      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
      services.AddScoped<IIngredientRepository, IngredientRepository>();
      services.AddScoped<IPlateRepository, PlateRepository>();
      services.AddScoped<IPlateCategoryRepository, PlateCategoryRepository>();
      services.AddScoped<IPlateIngredientRepository, PlateIngredientRepository>();
      services.AddScoped<IOrderRepository, OrderRepository>();
      services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
      services.AddScoped<ITableRepository, TableRepository>();
      services.AddScoped<ITableStatusRepository, TableStatusRepository>();
      
      #endregion
    }
  }
}