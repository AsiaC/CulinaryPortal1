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
        Task<Cookbook> GetCookbookWithRecipesAsync(int cookbookId);
        Task AddRecipeToCookbookAsync(CookbookRecipe cookbookRecipe, Cookbook cookbook);
        Task RemoveRecipeFromCookbookAsync(CookbookRecipe cookbookRecipe, Cookbook cookbook);
    }
}
