using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Application;
using Restaurant.Infrastructure.Identity;
using Restaurant.Infrastructure.Identity.Entities;
using Restaurant.Infrastructure.Identity.Seeds;
using Restaurant.Infrastructure.Persistence;
using Restaurant.Presentation.WebApi;
using Restaurant.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddApiControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope()) {
  var services = scope.ServiceProvider;

  try {
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await DefaultRoles.SeedAsync(userManager, roleManager);
    await DefaultAdminUser.SeedAsync(userManager, roleManager);
    await DefaultWaiterUser.SeedAsync(userManager, roleManager);
  } catch {

  }
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerExtension();
app.UseHealthChecks("/health");
app.UseSession();

app.UseEndpoints(endpoints => {
  endpoints.MapControllers();
});

app.Run();
