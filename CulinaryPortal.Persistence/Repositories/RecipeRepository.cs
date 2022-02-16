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
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(CulinaryPortalDbContext dbContext) : base(dbContext)
        { 
        }
        public async Task<List<Recipe>> GetRecipesWithDetailsAsync()
        {
            var recipes = await _dbContext.Recipes
                .Include(i => i.Instructions)
                .Include(p => p.Photos)                                         
                .Include(ing => ing.RecipeIngredients).ThenInclude(r => r.Ingredient)
                .Include(ing => ing.RecipeIngredients).ThenInclude(m => m.Measure)
                .Include(c => c.Category)
                .Include(r => r.Rates)
                .ToListAsync();

            return recipes;
        }

        public async Task<Recipe> GetRecipeWithDetailsAsync(int recipeId)
        {
            var recipe = await _dbContext.Recipes
                .Include(i => i.Instructions)
                //.Include(cr=>cr.CookbookRecipes).ThenInclude(c=>c.Cookbook)
                .Include(u => u.User)
                .Include(c => c.Category)
                .Include(p => p.Photos)                                         
                .Include(ing => ing.RecipeIngredients).ThenInclude(r => r.Ingredient)
                .Include(ing => ing.RecipeIngredients).ThenInclude(m => m.Measure)
                .Include(rate => rate.Rates)
                .FirstOrDefaultAsync(u => u.Id == recipeId);

            return recipe;
        }
    }
}
