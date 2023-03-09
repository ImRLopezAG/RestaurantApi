using Restaurant.Core.Application.Contracts;
using Restaurant.Infrastructure.Identity.Entities;
using Restaurant.Infrastructure.Identity.Services;
using Restaurant.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurant.Infrastructure.Identity;

public static class ServiceRegistration
{
  public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    #region Contexts
    if (configuration.GetValue<bool>("UseInMemoryDatabase"))
    {
      services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
    }
    else
    {
      services.AddDbContext<IdentityContext>(options =>
      {
        options.EnableSensitiveDataLogging();
        options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
        m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
      });
    }
    #endregion

    #region Identity
    services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

    services.ConfigureApplicationCookie(options =>
    {
      options.LoginPath = "/User";
      options.AccessDeniedPath = "/User/AccessDenied";
    });

    services.AddAuthentication();
    #endregion

    #region Services
    services.AddTransient<IAccountService, AccountService>();
    #endregion
  }
}

