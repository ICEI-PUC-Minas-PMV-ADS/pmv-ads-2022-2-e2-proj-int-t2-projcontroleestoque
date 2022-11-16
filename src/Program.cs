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

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(mySQLConnection, ServerVersion.AutoDetect(mySQLConnection));
        });

        // Add services to the container.
        builder.Services.AddControllersWithViews();

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

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}