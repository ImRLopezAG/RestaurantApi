using Restaurant.Core.Application.Core;
using Restaurant.Core.Application.Interfaces.Controllers;
using Restaurant.Presentation.WebApi.Controllers.v1;
using Restaurant.Presentation.WebApi.Core;

namespace Restaurant.Presentation.WebApi;

public static class ServiceRegistration {
  public static void AddApiControllers(this IServiceCollection services) {
    #region Controllers
    services.AddTransient(typeof(IGenericController<,,>), typeof(GenericController<,,>));
    services.AddTransient<IIngredientController, IngredientController>();
    services.AddTransient<IOrderController, OrderController>();
    services.AddTransient<IPlateController, PlateController>();
    services.AddTransient<ITableController, TableController>();
    #endregion
  }
}
