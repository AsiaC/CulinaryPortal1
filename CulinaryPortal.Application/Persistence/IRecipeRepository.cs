using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryPortal.Domain.Entities;

namespace CulinaryPortal.Application.Persistence
{
    public interface IRecipeRepository : IAsyncRepository<Recipe>
    {
        Task<List<Recipe>> GetRecipesWithDetailsAsync();
        Task<Recipe> GetRecipeWithDetailsAsync(int recipeId);
    }
}
