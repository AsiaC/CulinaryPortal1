using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryPortal.Domain.Entities;

namespace CulinaryPortal.Application.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<List<User>> GetUsersWithDetailsAsync();
        Task<Cookbook> GetUserCookbookAsync(int userId);
        Task<Rate> GetUserRecipeRateAsync(int userId, int recipeId);
        Task<List<Recipe>> SearchCookbookUserRecipesAsync(string name, int? categoryId, int? difficultyLevelId, int? preparationTimeId, int? userId);
    }
}
