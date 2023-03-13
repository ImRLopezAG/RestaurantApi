using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Application.Enums;
using Restaurant.Infrastructure.Identity.Entities;

namespace Restaurant.Infrastructure.Identity.Seeds;
public static class DefaultWaiterUser {
  public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) {
    ApplicationUser defaultUser = new() {
      UserName = "basicUser",
      Email = "basic@email.com",
      FirstName = "John",
      LastName = "Doe",
      EmailConfirmed = true,
      PhoneNumberConfirmed = true
    };

    if (userManager.Users.All(u => u.Id != defaultUser.Id)) {
      var user = await userManager.FindByEmailAsync(defaultUser.Email);
      if (user == null) {
        await userManager.CreateAsync(defaultUser, "123Pa$$word!");
        await userManager.AddToRoleAsync(defaultUser, Roles.Waiter.ToString());
      }
    }

  }
}

