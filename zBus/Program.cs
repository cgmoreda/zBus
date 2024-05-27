using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using zBus.Data;
using zBus.Data.Services;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis.Options;
using zBus.Filters;
using zBus.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<LoginAuthorizationFilter>();
builder.Services.AddScoped<RoleAuthorizationFilter>(provider => new RoleAuthorizationFilter("Admin"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true; // Make the session cookie essential
    options.IdleTimeout = TimeSpan.FromMinutes(3330); // Set session timeout to 20 minutes
});

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
builder.Services.AddScoped<ISeatsService, SeatService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

AppDbInitializer.seed(app);

app.Run();
