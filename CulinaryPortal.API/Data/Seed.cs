using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using CulinaryPortal.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CulinaryPortal.API.Data
{
    public class Seed
    {
        public static async Task SeedInitialData(UserManager<User> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Member"}                
            };
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            var users = new List<User>
            {
                new User{FirstName = "Joanna", LastName = "Czaplicka", Email = "jczaplicka@wp.pl", IsActive = true, UserName = "jczaplicka"},
                new User{FirstName = "Anna", LastName = "Nowak", Email = "anowak@wp.pl", IsActive = true, UserName = "anowak"}
            };
            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "123");
                await userManager.AddToRoleAsync(user, "Member");
            }           
        }
    }
}
