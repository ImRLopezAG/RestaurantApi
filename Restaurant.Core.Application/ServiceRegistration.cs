using Microsoft.Extensions.DependencyInjection;
using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Contracts.Core;
using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Services;
using System.Reflection;

namespace Restaurant.Core.Application;

public static class ServiceRegistration {
  public static void AddApplicationServices(this IServiceCollection services) {
    services.AddAutoMapper(Assembly.GetExecutingAssembly());

    #region Services
    services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
    services.AddTransient<IUserService, UserService>();
    services.AddTransient<IPlateService, PlateService>();
    services.AddTransient<IIngredientService, IngredientService>();
    services.AddTransient<IOrderService, OrderService>();
    services.AddTransient<ITableService, TableService>();

    #endregion
  }
}
