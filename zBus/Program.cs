using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using zBus.Data;
using zBus.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// DbContext configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Default connection string not found in appsettings.json");
}

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// add IDriversService to the container
builder.Services.AddScoped<IDriversService, DriversService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddScoped<IBusService, BusService>();
builder.Services.AddScoped<ITripService, TripService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

AppDbInitializer.seed(app);

app.Run();
