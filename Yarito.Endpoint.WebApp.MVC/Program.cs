using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yarito.Domain.AppServices.Requests;
using Yarito.Domain.AppServices.Users;
using Yarito.Domain.AppServices.Works;
using Yarito.Domain.Core.Contracts.Cities.Repository;
using Yarito.Domain.Core.Contracts.Requests.AppServices;
using Yarito.Domain.Core.Contracts.Requests.Repository;
using Yarito.Domain.Core.Contracts.Requests.Services;
using Yarito.Domain.Core.Contracts.Users.AppServices;
using Yarito.Domain.Core.Contracts.Users.Repository;
using Yarito.Domain.Core.Contracts.Users.Services;
using Yarito.Domain.Core.Contracts.Works.AppServices;
using Yarito.Domain.Core.Contracts.Works.Repository;
using Yarito.Domain.Core.Contracts.Works.Services;
using Yarito.Domain.Services.Requests;
using Yarito.Domain.Services.Users;
using Yarito.Domain.Services.Works;
using Yarito.Framework;
using Yarito.Infra.DataAccess.EFCore.Cities;
using Yarito.Infra.DataAccess.EFCore.Requests;
using Yarito.Infra.DataAccess.EFCore.Users;
using Yarito.Infra.DataAccess.EFCore.Works;
using Yarito.Infra.Database.SQLServer.EFCore.DatabaseContext;
using Yarito.Infra.Database.SQLServer.Identity.DatabaseContext;

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


// Identity Configuration
builder.Services
    .AddIdentity<IdentityUser<int>, IdentityRole<int>>(options =>
    {
        // Password
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;

        // User
        options.User.AllowedUserNameCharacters = "0123456789";
        options.User.RequireUniqueEmail = false;

        // SignIn
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
    })
    .AddEntityFrameworkStores<IdentityAppDbContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<PersianIdentityErrors>();




// Cookie settings Configuration
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Home/Index";

    options.ExpireTimeSpan = TimeSpan.FromDays(14);
    options.SlidingExpiration = true;

    options.Cookie.Name = "Yarito.Auth";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});


// Global Anti Forgery Token Validation
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});



// Dependency Injection for AppServices
builder.Services.AddScoped<IReviewsAppServices, ReviewsAppServices>();
builder.Services.AddScoped<ICategoryAppServices, CategoryAppServices>();
builder.Services.AddScoped<IAuthenticationAppServices, AuthenticationAppServices>();
builder.Services.AddScoped<IAppUserAppServices, AppUserAppServices>();
builder.Services.AddScoped<IRequestAppServices, RequestAppServices>();
builder.Services.AddScoped<IBidAppServices, BidAppServices>();

// Dependency Injection for Services
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IReviewsServices, ReviewsServices>();
builder.Services.AddScoped<IAppUserServices, AppUserServices>();
builder.Services.AddScoped<IRequestServices, RequestServices>();
builder.Services.AddScoped<IBidServices, BidServices>();

// Dependency Injection for Repositories
builder.Services.AddScoped<ICityRepo, CityRepo>();
builder.Services.AddScoped<IBidRepo, BidRepo>();
builder.Services.AddScoped<IRequestRepo, RequestRepo>();
builder.Services.AddScoped<IReviewRepo, ReviewRepo>();
builder.Services.AddScoped<IAppUserRepo, AppUserRepo>();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();


app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
