using CulinaryPortal.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Services
{
    public interface ICulinaryPortalRepository
    {
        bool Save();

        IEnumerable<Recipe> GetUserRecipes(int userId);
        Recipe GetUserRecipe(int userId, int recipeId);
        void AddRecipe(int userId, Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        void DeleteRecipe(Recipe recipe);

        IEnumerable<User> GetUsers();
        User GetUser(int userId);
        IEnumerable<User> GetUsers(IEnumerable<int> userIds);
        void AddUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        bool UserExists(int userId);
        
        //ndk

    }
}
