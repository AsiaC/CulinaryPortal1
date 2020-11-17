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
        Task SaveChangesAsync();

        #region Recipe


        IEnumerable<Recipe> GetUserRecipes(int userId);
        Recipe GetUserRecipe(int userId, int recipeId);
        void AddRecipe(int userId, Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        void DeleteRecipe(Recipe recipe);
        bool RecipeExists(int recipeId);
        IEnumerable<Recipe> GetRecipes();
        Recipe GetRecipe(int recipeId);
        void AddRecipe(Recipe recipe);
        #endregion

        #region User


        IEnumerable<User> GetUsers();
        User GetUser(int userId);
        Task<User> GetUserAsync(string username);
        IEnumerable<User> GetUsers(IEnumerable<int> userIds);
        void AddUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        bool UserExists(int userId);
        Task<bool> UserExists(string username);
        #endregion

        #region Ingredient
        IEnumerable<Ingredient> GetIngredients();
        bool IngredientExists(int ingredientId);
        Ingredient GetIngredient(int ingredientId);
        void AddIngredient(Ingredient ingredient);
        void DeleteIngredient(Ingredient ingredient);

        #endregion

        #region Measure
        IEnumerable<Measure> GetMeasures();

        bool MeasureExists(int measureId);
        Measure GetMeasure(int measureId);
        void AddMeasure(Measure measure);
        void DeleteMeasure(Measure measure);

        #endregion

        #region Instruction
        IEnumerable<Instruction> GetInstructions();
        bool InstructionExists(int instructionId);
        Instruction GetInstruction(int instructionId);
        void AddInstruction(Instruction instruction);
        void DeleteInstruction(Instruction instruction);
        #endregion

        #region ShoppingList
        IEnumerable<ShoppingList> GetShoppingLists();
        bool ShoppingListExists(int shoppingListId);
        ShoppingList GetShoppingList(int shoppingListId);
        void AddShoppingList(ShoppingList shoppingList);
        void DeleteShoppingList(ShoppingList shoppingList);

        #endregion

        #region Cookbook
        IEnumerable<Cookbook> GetCookbooks();
        bool CookbookExists(int cookbookId);
        Cookbook GetCookbook(int cookbookId);
        void AddCookbook(Cookbook cookbook);
        void DeleteCookbook(Cookbook cookbook);

        #endregion
    }
}
