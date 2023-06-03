using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PUConWebHackathonProject.Models;
using PUConWebHackathonProject.Models.IRepositories;
using PUConWebHackathonProject.Models.Repositories;
using PUConWebHackathonProject.Models.Repositories.Identity;

var builder = WebApplication.CreateBuilder(args);
string connString = @$"Data Source=(localdb)\MSSQLLocalDB;Database=PUConDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

var migrationAssembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddDbContext<IdentityContext>(options =>
options.UseSqlServer(connString, sql => sql.MigrationsAssembly(migrationAssembly)));
builder.Services.AddDbContext<EventsContext>(options => 
options.UseSqlServer(connString, sql => sql.MigrationsAssembly(migrationAssembly)));



builder.Services.Configure<IdentityOptions>(options => options.User.RequireUniqueEmail = true) ;
builder.Services.AddIdentity<IdentityModel, IdentityRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();
builder.Services.AddScoped(typeof(IEventsRepository<>), typeof(EventsRepository<>));
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

var scope = app.Services.CreateScope();

var migIdentityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
migIdentityContext.Database.MigrateAsync().Wait();

var migEventsContext = scope.ServiceProvider.GetRequiredService<EventsContext>();
migEventsContext.Database.MigrateAsync().Wait();

var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

if (!await roleManager.RoleExistsAsync("Admin"))
{
    var adminRole = new IdentityRole("Admin");
    await roleManager.CreateAsync(adminRole);
}

// Check if the "User" role exists and create it if it doesn't
if (!await roleManager.RoleExistsAsync("User"))
{
    var userRole = new IdentityRole("User");
    await roleManager.CreateAsync(userRole);
}

var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityModel>>();
var adminUser = await userManager.FindByNameAsync("admin@pucon");
if (adminUser == null)
{
    adminUser = new IdentityModel
    {
        User_Name = "admin@pucon",
        FirstName="Admin",
        LastName="Pucon",
        User_Email = "admin@pucon.com",
        EmailConfirmed = true,
        LockoutEnabled = false,
    };
    var result = await userManager.CreateAsync(adminUser, "Pucon@123");
    if (result.Succeeded)
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
