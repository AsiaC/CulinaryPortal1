using CulinaryPortal.Domain.Entities;
using CulinaryPortal.Persistence;
using CulinaryPortal.Persistence.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.ApplicationProgrammingInterface
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();//.Run();
            // migrate the database.  Best practice = in Main, using service scope
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetService<CulinaryPortalDbContext>();
                    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    //// for demo purposes, delete the database & migrate on startup so we can start with a clean state
                    //await context.Database.EnsureDeletedAsync(); // Drop the database if it exists
                    //await context.Database.MigrateAsync();
                    await Seed.SeedInitialDataAsync(userManager, roleManager, context);                    
                }
                catch (Exception ex) //TODO WYCZYSC
                {
                    //var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    //logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            // run the web app
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
