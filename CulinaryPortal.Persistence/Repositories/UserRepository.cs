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

        public async Task<Cookbook> GetUserCookbookAsync(int userId)
        {
            var cookbook = await _dbContext.Cookbooks
                .Include(c => c.CookbookRecipes).ThenInclude(r => r.Recipe).ThenInclude(c => c.Category)
                .Include(c => c.CookbookRecipes).ThenInclude(r => r.Recipe).ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            return cookbook;
        }               

        public async Task<List<Recipe>> SearchCookbookUserRecipesAsync(string name, int? categoryId, int? difficultyLevelId, int? preparationTimeId, int? userId)
        {
            var cookbook = await _dbContext.Cookbooks
                .Include(c => c.CookbookRecipes).ThenInclude(r => r.Recipe).ThenInclude(c => c.Category)
                .Include(c => c.CookbookRecipes).ThenInclude(r => r.Recipe).ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            IEnumerable<Recipe> query = cookbook.CookbookRecipes.Select(r => r.Recipe);

            if (categoryId != null)
            {
                query = query.Where(r => r.CategoryId == categoryId);
            }

            if (preparationTimeId != null)
            {
                query = query.Where(r => (int)r.PreparationTime <= preparationTimeId);
            }

            if (difficultyLevelId != null)
            {
                query = query.Where(r => (int)r.DifficultyLevel == difficultyLevelId);
            }

            if (!String.IsNullOrWhiteSpace(name))
            {
                query = query.Where(r => r.Name.ToLower() == name.ToLower());
            }
            return query.ToList();
        }
    }
}
