﻿using CulinaryPortal.API.Entities;
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
        void AddRecipe(int userId, Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        void DeleteRecipe(Recipe recipe);
        Task<bool> RecipeExistsAsync(int recipeId);
        Task<IEnumerable<Recipe>> GetRecipesAsync();
        Task<Recipe> GetRecipeAsync(int recipeId);
        void AddRecipe(Recipe recipe);
        #endregion

        #region User
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int userId);
        Task<User> GetUserAsync(string username);
        Task<IEnumerable<User>> GetUsersAsync(IEnumerable<int> userIds);
        void AddUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        Task<bool> UserExistsAsync(int userId);
        Task<bool> UserExistsAsync(string username);
        Task<Cookbook> GetUserCookbookAsync(int userId);
        #endregion

        #region Ingredient
        Task<IEnumerable<Ingredient>> GetIngredientsAsync();
        Task<bool> IngredientExistsAsync(int ingredientId);
        Task<Ingredient> GetIngredientAsync(int ingredientId);
        void AddIngredient(Ingredient ingredient);
        void DeleteIngredient(Ingredient ingredient);
        #endregion

        #region Measure
        Task<IEnumerable<Measure>> GetMeasuresAsync();
        Task<bool> MeasureExistsAsync(int measureId);
        Task<Measure> GetMeasureAsync(int measureId);
        void AddMeasure(Measure measure);
        void DeleteMeasure(Measure measure);

        #endregion

        #region Instruction
        Task<IEnumerable<Instruction>> GetInstructionsAsync();
        Task<bool> InstructionExistsAsync(int instructionId);
        Task<Instruction> GetInstructionAsync(int instructionId);
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
        Task<IEnumerable<Cookbook>> GetCookbooksAsync();
        Task<bool> CookbookExistsAsync(int cookbookId);
        Task<Cookbook> GetCookbookAsync(int cookbookId);
        void AddCookbook(Cookbook cookbook);
        void DeleteCookbook(Cookbook cookbook);
        
        #endregion
    }
}
