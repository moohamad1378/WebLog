using EndPoint.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
ConfigurationManager Configuration = builder.Configuration;
#region DataBase
builder.Services.AddDbContext<DataBaseContext>(opt =>
opt.UseSqlServer(Configuration["ConnectionStrings:SqlServer1"]));
#endregion
builder.Services.AddAuthorization();
#region MicIdentity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<DataBaseContext>()
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddErrorDescriber<CustomIdentityError>();
/////
builder.Services.Configure<IdentityOptions>(option =>
{

    option.User.RequireUniqueEmail = true;


    option.Password.RequireDigit = true;
    option.Password.RequireLowercase = true;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequireUppercase = true;
    option.Password.RequiredLength = 8;
    option.Password.RequiredUniqueChars = 1;

    //Lokout Setting
    option.Lockout.MaxFailedAccessAttempts = 10;
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(10);

    //SignIn Setting
    option.SignIn.RequireConfirmedAccount = false;
    option.SignIn.RequireConfirmedEmail = false;
    option.SignIn.RequireConfirmedPhoneNumber = false;

});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(999);
    options.LoginPath = "/Account/login";
    options.AccessDeniedPath = "/Account/Accessdenied";
    options.SlidingExpiration = true;
});
#endregion




var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
