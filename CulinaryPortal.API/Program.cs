using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CulinaryPortal.API.Data;
using CulinaryPortal.API.DbContexts;
using CulinaryPortal.API.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CulinaryPortal.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // migrate the database.  Best practice = in Main, using service scope
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetService<CulinaryPortalContext>();
                    // for demo purposes, delete the database & migrate on startup so we can start with a clean state
                    //context.Database.EnsureDeleted();
                    //context.Database.Migrate();                    
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    await Seed.SeedInitialData(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
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
