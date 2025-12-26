using Yarito.Infra.Database.SQLServer.Identity.DatabaseContext;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;
using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.Contracts.Cities.Repository;
using Yarito.Domain.Core.Contracts.Works.Repository;
using Yarito.Domain.Core.Contracts.Users.Repository;
using Yarito.Infra.DataAccess.EFCore.Requests;
using Yarito.Infra.DataAccess.EFCore.Cities;
using Yarito.Infra.DataAccess.EFCore.Works;
using Yarito.Infra.DataAccess.EFCore.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//Database Connection String
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<IdentityAppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("IdentityConnection")));


// Dependency Injection for AppServices

// Dependency Injection for Services

// Dependency Injection for Repositories
builder.Services.AddScoped<ICityRepo, CityRepo>();
builder.Services.AddScoped<IBidRepo, BidRepo>();
builder.Services.AddScoped<IRequestRepo, RequestRepo>();
builder.Services.AddScoped<IReviewRepo, ReviewRepo>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<IExpertRepo, ExpertRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IWorkRepo, WorkRepo>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
