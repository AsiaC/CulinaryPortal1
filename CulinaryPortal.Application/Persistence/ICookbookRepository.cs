using CulinaryPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Persistence
{
    public interface ICookbookRepository : IAsyncRepository<Cookbook>
    {
        Task<List<Cookbook>> GetCookbooksWithRecipesAsync();
        Task<Cookbook> GetUserCookbookAsync(int userId);
        Task AddRecipeToCookbookAsync(CookbookRecipe cookbookRecipe, Cookbook cookbook);
        Task RemoveRecipeFromCookbookAsync(CookbookRecipe cookbookRecipe, Cookbook cookbook);
        Task<Cookbook> GetCookbookWithDetailsAsync(int cookbookId);
        Task<int> CountAssociatedCookbooksAsync(int recipeId);
        Task<List<Recipe>> SearchCookbookUserRecipesAsync(string name, int? categoryId, int? difficultyLevelId, int? preparationTimeId, int? userId);
    }
}
