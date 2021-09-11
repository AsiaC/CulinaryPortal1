using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
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
        Task<bool> SaveAllAsync();

        #region Recipe
        Task<IEnumerable<Recipe>> GetUserRecipesAsync(int userId);
        Task<Recipe> GetUserRecipeAsync(int userId, int recipeId);    
        Task DeleteRecipeAsync(Recipe recipe);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<Recipe>> GetRecipesAsync();
        Task<Recipe> GetRecipeAsync(int recipeId);
        Task AddRecipeAsync(Recipe recipe);
        Task<int> CountAssociatedCookbooksAsync(int recipeId);
        Task<IEnumerable<Recipe>> SearchRecipesAsync(SearchRecipeDto searchRecipeDto);
        #endregion

        #region User
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int userId);
        Task<User> GetUserAsync(string username);
        Task<IEnumerable<User>> GetUsersAsync(IEnumerable<int> userIds);
        Task AddUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task UpdateUserAsync(User user);

        //czy to jest potrzebne a nie wystarczyłoby getCookbookAsync?
        Task<Cookbook> GetUserCookbookAsync(int userId);
        Task<IEnumerable<Recipe>> SearchUserCookbookRecipesAsync(SearchRecipeDto searchRecipeDto);
        #endregion

        #region Ingredient
        Task<IEnumerable<Ingredient>> GetIngredientsAsync();
        Task<Ingredient> GetIngredientAsync(int ingredientId);
        Task AddIngredientAsync(Ingredient ingredient);
        Task DeleteIngredientAsync(Ingredient ingredient);        
        #endregion

        #region Measure
        Task<IEnumerable<Measure>> GetMeasuresAsync();
        Task<Measure> GetMeasureAsync(int measureId);
        Task AddMeasureAsync(Measure measure);
        Task DeleteMeasureAsync(Measure measure);

        #endregion

        #region Instruction
        Task<IEnumerable<Instruction>> GetInstructionsAsync();
        Task<Instruction> GetInstructionAsync(int instructionId);
        Task AddInstructionAsync(Instruction instruction);
        Task DeleteInstructionAsync(Instruction instruction);
        //void UpdateInstruction(Instruction instruction);
        #endregion

        #region ShoppingList
        Task<IEnumerable<ShoppingList>> GetShoppingListsAsync();
        Task<ShoppingList> GetShoppingListAsync(int shoppingListId);
        Task AddShoppingListAsync(ShoppingList shoppingList);
        Task DeleteShoppingListAsync(ShoppingList shoppingList);
        #endregion

        #region Cookbook
        Task<IEnumerable<Cookbook>> GetCookbooksAsync();
        Task<Cookbook> GetCookbookAsync(int cookbookId);
        Task AddCookbookAsync(Cookbook cookbook);
        Task DeleteCookbookAsync(Cookbook cookbook);
        Task<IEnumerable<ShoppingList>> GetUserShoppingListsAsync(int userId);
        #endregion

    }
}
