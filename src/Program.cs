using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using ProjControleEstoque.Context;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var mySQLConnection = builder.Configuration.GetConnectionString("LocalConnection");


        
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddSession(option =>
        {
            option.IdleTimeout = TimeSpan.FromMinutes(40);
        });

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(mySQLConnection, ServerVersion.AutoDetect(mySQLConnection));
        });

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Employee", policy => policy.RequireClaim("Employee"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();
        app.UseSession();

        app.UseRouting();

        app.UseAuthorization();        

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}