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

        public async Task<List<Recipe>> GetUserRecipesAsync(int userId)
        {
            var userRecipes = await _dbContext.Recipes.Where(r => r.UserId == userId)
                .Include(i => i.Instructions)
                .Include(p => p.Photos)
                .Include(ing => ing.RecipeIngredients).ThenInclude(r => r.Ingredient)
                .Include(ing => ing.RecipeIngredients).ThenInclude(m => m.Measure)
                .Include(c => c.Category)
                .ToListAsync();
            return userRecipes;
        }

        public async Task<List<ShoppingList>> GetUserShoppingListsAsync(int userId)
        {
            var userShoppingLists = await _dbContext.ShoppingLists.Where(r => r.UserId == userId)
                .Include(l => l.Items)
                .ToListAsync();
            return userShoppingLists;
        }

        public async Task<Rate> GetUserRecipeRateAsync(int userId, int recipeId)
        {
            return await _dbContext.Rates.FirstOrDefaultAsync(r => r.RecipeId == recipeId && r.UserId == userId);
        }
    }
}
