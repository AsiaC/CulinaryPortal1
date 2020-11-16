using CulinaryPortal.API.DbContexts;
using CulinaryPortal.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.Services
{
    public class CulinaryPortalRepository : ICulinaryPortalRepository, IDisposable
    {
        private readonly CulinaryPortalContext _context;
        public CulinaryPortalRepository(CulinaryPortalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        #region Recipe

        public void AddRecipe(int userId, Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe));
            }
            // always set the AuthorId to the passed-in authorId
            recipe.UserId = userId;
            _context.Recipes.Add(recipe);
        }
        public void DeleteRecipe(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
        }

        public Recipe GetUserRecipe(int userId, int recipeId)
        {
            return _context.Recipes.FirstOrDefault(r => r.Id == recipeId && r.UserId == userId);
        }

        public IEnumerable<Recipe> GetUserRecipes(int userId)
        {
            return _context.Recipes.Where(r => r.UserId == userId).ToList();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            // no code in this implementation
        }

        public bool RecipeExists(int recipeId)
        {
            return _context.Recipes.Any(u => u.Id == recipeId);
        }
        public IEnumerable<Recipe> GetRecipes()
        {
            return _context.Recipes.ToList();
        }
        public Recipe GetRecipe(int recipeId)
        {
            return _context.Recipes.FirstOrDefault(u => u.Id == recipeId);
        }

        public void AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe));
            }

            _context.Recipes.Add(recipe);
        }
        #endregion

        #region User
        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList<User>();
        }

        public User GetUser(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }               

        public IEnumerable<User> GetUsers(IEnumerable<int> userIds)
        {
            if (userIds == null)
            {
                throw new ArgumentNullException(nameof(userIds));
            }
            return _context.Users.Where(u => userIds.Contains(u.Id)).OrderBy(u => u.LastName).ThenBy(u => u.FirstName).ToList();
        }         

        public void UpdateUser(User user)
        {
            // no code in this implementation
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username.ToLower());
        }
        #endregion

        #region Ingredient
        public IEnumerable<Ingredient> GetIngredients()
        {
            return _context.Ingredients.ToList();
        }

        public bool IngredientExists(int ingredientId)
        {
            return _context.Ingredients.Any(u => u.Id == ingredientId);
        }

        public Ingredient GetIngredient(int ingredientId)
        {
            return _context.Ingredients.FirstOrDefault(u => u.Id == ingredientId);
        }

        public void AddIngredient(Ingredient ingredient)
        {
            if (ingredient == null)
            {
                throw new ArgumentNullException(nameof(ingredient));
            }

            _context.Ingredients.Add(ingredient);
        }

        public void DeleteIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Remove(ingredient);
        }
        #endregion

        #region Measure
        public IEnumerable<Measure> GetMeasures()
        {
            return _context.Measures.ToList();
        }

        public bool MeasureExists(int measureId)
        {
            return _context.Measures.Any(u => u.Id == measureId);
        }

        public Measure GetMeasure(int measureId)
        {
            return _context.Measures.FirstOrDefault(u => u.Id == measureId);
        }

        public void AddMeasure(Measure measure)
        {
            if (measure == null)
            {
                throw new ArgumentNullException(nameof(measure));
            }

            _context.Measures.Add(measure);
        }

        public void DeleteMeasure(Measure measure)
        {
            _context.Measures.Remove(measure);
        }
        #endregion

        #region Instruction


        public IEnumerable<Instruction> GetInstructions()
        {
            return _context.Instructions.ToList();
        }

        public bool InstructionExists(int instructionId)
        {
            return _context.Instructions.Any(u => u.Id == instructionId);
        }

        public Instruction GetInstruction(int instructionId)
        {
            return _context.Instructions.FirstOrDefault(u => u.Id == instructionId);
        }

        public void AddInstruction(Instruction instruction)
        {
            if (instruction == null)
            {
                throw new ArgumentNullException(nameof(instruction));
            }

            _context.Instructions.Add(instruction);
        }

        public void DeleteInstruction(Instruction instruction)
        {
            _context.Instructions.Remove(instruction);
        }
        #endregion

        #region ShoppingList


        public IEnumerable<ShoppingList> GetShoppingLists()
        {
            return _context.ShoppingLists.ToList();
        }

        public bool ShoppingListExists(int shoppingListId)
        {
            return _context.ShoppingLists.Any(u => u.Id == shoppingListId);
        }

        public ShoppingList GetShoppingList(int shoppingListId)
        {
            return _context.ShoppingLists.FirstOrDefault(u => u.Id == shoppingListId);
        }

        public void AddShoppingList(ShoppingList shoppingList)
        {
            if (shoppingList == null)
            {
                throw new ArgumentNullException(nameof(shoppingList));
            }

            _context.ShoppingLists.Add(shoppingList);
        }

        public void DeleteShoppingList(ShoppingList shoppingList)
        {
            _context.ShoppingLists.Remove(shoppingList);
        }
        #endregion

        #region Cookbook


        public IEnumerable<Cookbook> GetCookbooks()
        {
            return _context.Cookbooks.ToList();
        }

        public bool CookbookExists(int cookbookId)
        {
            return _context.Cookbooks.Any(u => u.Id == cookbookId);
        }

        public Cookbook GetCookbook(int cookbookId)
        {
            return _context.Cookbooks.FirstOrDefault(u => u.Id == cookbookId);
        }

        public void AddCookbook(Cookbook cookbook)
        {
            if (cookbook == null)
            {
                throw new ArgumentNullException(nameof(cookbook));
            }

            _context.Cookbooks.Add(cookbook);
        }

        public void DeleteCookbook(Cookbook cookbook)
        {
            _context.Cookbooks.Remove(cookbook);
        }




        #endregion
    }
}
