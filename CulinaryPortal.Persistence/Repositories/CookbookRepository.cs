﻿using CulinaryPortal.Application.Persistence;
using CulinaryPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Persistence.Repositories
{
    public class CookbookRepository : BaseRepository<Cookbook>, ICookbookRepository
    {
        public CookbookRepository(CulinaryPortalDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Cookbook>> GetCookbooksWithRecipesAsync()
        {
            var cookbooks = await _dbContext.Cookbooks
                .Include(c => c.CookbookRecipes).ThenInclude(r => r.Recipe).ThenInclude(p => p.Photos)
                .Include(c => c.User)
                .ToListAsync();

            return cookbooks;
        }
        public async Task<Cookbook> GetUserCookbookAsync(int userId)
        {
            var cookbook = await _dbContext.Cookbooks
                .Include(c => c.CookbookRecipes).ThenInclude(r => r.Recipe).ThenInclude(c => c.Category)
                .Include(c => c.CookbookRecipes).ThenInclude(r => r.Recipe).ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            return cookbook;
        }
        public async Task AddRecipeToCookbookAsync(CookbookRecipe cookbookRecipe, Cookbook cookbook)
        {
            cookbook.CookbookRecipes.Add(cookbookRecipe);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveRecipeFromCookbookAsync(CookbookRecipe cookbookRecipe, Cookbook cookbook)
        {
            var itemToRemove = cookbook.CookbookRecipes.FirstOrDefault(c=>c.CookbookId == cookbookRecipe.CookbookId && c.RecipeId == cookbookRecipe.RecipeId);
            if (itemToRemove != null)
            {
                cookbook.CookbookRecipes.Remove(itemToRemove);
                await _dbContext.SaveChangesAsync();
            }            
        }
        public async Task<Cookbook> GetCookbookWithDetailsAsync(int cookbookId)
        {
            var cookbook = await _dbContext.Cookbooks
                .Include(c => c.CookbookRecipes)//.ThenInclude(r => r.Recipe)
                .FirstOrDefaultAsync(u => u.Id == cookbookId);
            return cookbook;
        }
        public async Task<int> CountAssociatedCookbooksAsync(int recipeId)
        {
            return await _dbContext.Cookbooks.SelectMany(x => x.CookbookRecipes.Where(a => a.RecipeId == recipeId)).CountAsync();            
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
