using CulinaryPortal.Application.Persistence;
using CulinaryPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CulinaryPortalDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<User>> GetUsersWithDetailsAsync()
        {
            var users = await _dbContext.Users
                .Include(r => r.Recipes)
                .ToListAsync();

            var allCookbooks = await _dbContext.Cookbooks.ToListAsync();

            foreach (var user in users)
            {
                if (user.Cookbook != null)
                {
                    user.Cookbook = allCookbooks.FirstOrDefault(c => c.Id == user.Cookbook.Id);
                }
            }
            return users;
        }
    }
}
