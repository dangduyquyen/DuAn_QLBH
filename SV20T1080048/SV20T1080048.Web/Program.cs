using SV20T1080048.Web;
using SV20T1080048.Web.AddCodes;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Session;

var builder = WebApplication.CreateBuilder(args);

// Bổ sung các service cần dùng:
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews()
    .AddMvcOptions(option =>
    {
        option.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; //Không sử dụng thông báo mặc định cho giá trị null                    
    });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
    {
        option.Cookie.Name = "AuthenticationCookie";
        option.LoginPath = "/Account/Login";
        option.AccessDeniedPath = "/Account/AccessDenied";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

builder.Services.AddSession(option =>
    {
        option.IdleTimeout = TimeSpan.FromMinutes(60);
        option.Cookie.HttpOnly = true;
        option.Cookie.IsEssential = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();


app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "areaAdmin",
        areaName: "Admin",
        pattern: "admin/{controller=Dashboard}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Khởi tạo cấu hình cho ApplicationContext
ApplicationContext.Configure
(
    httpContextAccessor: app.Services.GetRequiredService<IHttpContextAccessor>(),
    hostEnvironment: app.Services.GetService<IWebHostEnvironment>()
);

app.Run();