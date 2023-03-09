using Restaurant.Core.Application.Enums;
using Restaurant.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Restaurant.Infrastructure.Identity.Seeds;
public static class DefaultRoles
{
  public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
  {
    await roleManager.CreateAsync(new IdentityRole(Roles.Bartender.ToString()));
    await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
  }
}

