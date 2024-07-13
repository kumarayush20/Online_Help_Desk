using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Onlinehelpdesk.Data;
using Onlinehelpdesk.Models;
using Onlinehelpdesk.Securiity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<Onlinehelpdeskdb>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Onlinehelpdesk")));

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Login/Index";
                options.ForwardSignOut = "/Login/SignOut";
                options.AccessDeniedPath = "/Login/AccessDenied";
            });
        //builder.Services.AddAuthorization(options =>
        //{
        //    options.AddPolicy("AdminPolicy", policy =>
        //        policy.RequireRole("Administrator"));
        //    options.AddPolicy("SupportPolicy", policy =>
        //        policy.RequireRole("Support"));
        //    options.AddPolicy("EmployeePolicy", policy =>
        //        policy.RequireRole("Employee"));
        //});
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator"));
            options.AddPolicy("RequireSupportRole", policy => policy.RequireRole("Support"));
            options.AddPolicy("RequireEmployeeRole", policy => policy.RequireRole("Employee"));
        });
        builder.Services.AddScoped<SecurityManager>(); // Register your SecurityManager class
       builder.Services.AddControllersWithViews();
    
        
        builder.Services.AddSession();

        // This is required for MVC
        builder.Services.AddMvc().AddSessionStateTempDataProvider();

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

        app.UseSession(); // Place this before UseAuthentication and UseAuthorization
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");
        });

        app.Run();
    }
}