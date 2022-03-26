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

        public async Task<List<Photo>> GetRecipePhotosAsync(int recipeId)
        {
            return await _dbContext.Photos.Where(p => p.RecipeId == recipeId).ToListAsync();
        }

        public async Task AddPhotoAsync(Photo photo)
        {
            if (photo == null)
            {
                throw new ArgumentNullException(nameof(photo));
            }

            await _dbContext.Photos.AddAsync(photo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Recipe>> SearchRecipesAsync(string name, int? categoryId, int? difficultyLevelId, int? preparationTimeId, int? userId, int? top)
        {
            IEnumerable<Recipe> query = await _dbContext.Recipes
                .Include(i => i.Instructions)
                .Include(p => p.Photos)
                .Include(ing => ing.RecipeIngredients).ThenInclude(r => r.Ingredient)
                .Include(ing => ing.RecipeIngredients).ThenInclude(m => m.Measure)
                .Include(c => c.Category)
                .Include(r => r.Rates)
                .ToListAsync();

            if (userId != null)
            {
                query = query.Where(r => r.UserId == userId);
            }

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
