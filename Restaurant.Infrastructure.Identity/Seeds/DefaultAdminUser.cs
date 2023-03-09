﻿using Restaurant.Core.Application.Enums;
using Restaurant.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Restaurant.Infrastructure.Identity.Seeds;
public static class DefaultAdminUser
{
  public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
  {
    ApplicationUser defaultUser = new()
    {
      UserName = "AdminUser",
      Email = "adminUser@email.com",
      FirstName = "John",
      LastName = "Doe",
      EmailConfirmed = true,
      PhoneNumberConfirmed = true
    };

    if (userManager.Users.All(u => u.Id != defaultUser.Id))
    {
      var user = await userManager.FindByEmailAsync(defaultUser.Email);
      if (user == null)
      {
        await userManager.CreateAsync(defaultUser, "123Pa$$word!");
        await userManager.AddToRoleAsync(defaultUser, Roles.Bartender.ToString());
        await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
      }
    }

  }
}
