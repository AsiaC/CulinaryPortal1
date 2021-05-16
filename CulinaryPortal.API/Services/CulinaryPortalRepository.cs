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

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0; //wiecej niz 0 zmian zostało zapisanych w bazie. Jesli coś zostanie zapisane w bazie to wartość bedzie wieksza niz 0
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

        public async Task<Recipe> GetUserRecipeAsync(int userId, int recipeId)
        {
            var userRecipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId && r.UserId == userId);
            return userRecipe;
        }

        public async Task<IEnumerable<Recipe>> GetUserRecipesAsync(int userId)
        {
            var userRecipes = await _context.Recipes.Where(r => r.UserId == userId)
                .Include(i => i.Instructions)
                .Include(p => p.Photos) //ODKOMENTOWAĆ
                .Include(ing => ing.RecipeIngredients).ThenInclude(r => r.Ingredient)
                .Include(ing => ing.RecipeIngredients).ThenInclude(m => m.Measure)
                .ToListAsync();
            return userRecipes;
        }

        public void UpdateRecipe(Recipe recipe)
        {           
            _context.Entry(recipe).State = EntityState.Modified;
            //_context.Recipes.Update(recipe);            
        }

        public async Task<bool> RecipeExistsAsync(int recipeId)
        {
            var isExist = await _context.Recipes.AnyAsync(u => u.Id == recipeId);
            return isExist;
        }
        public async Task<IEnumerable<Recipe>> GetRecipesAsync()
        {
            var recipes = await _context.Recipes
                .Include(i => i.Instructions)
                .Include(p=>p.Photos) //ODKOMENTOWAĆ
                //.Include(cr => cr.CookbookRecipes).ThenInclude(c => c.Cookbook)
                .Include(ing=> ing.RecipeIngredients).ThenInclude(r=>r.Ingredient)
                .Include(ing => ing.RecipeIngredients).ThenInclude(m => m.Measure)
                .ToListAsync();

            //var recipes1 = await _context.Recipes
            //    .Include(i => i.Instructions)
            //    .Include(u => u.User)
            //    .Include(cb => cb.CookbookRecipes)
            //    .ToListAsync();

            //var recipes2 = await _context.Recipes
            //    .Include(i => i.Instructions)
            //    .Include(u => u.User)
            //    .ToListAsync();

            return recipes;
        }

        public async Task<Recipe> GetRecipeAsync(int recipeId)
        {
            //var recipe = _context.Recipes.FirstOrDefault(u => u.Id == recipeId);
            var recipe = await _context.Recipes
                .Include(i =>i.Instructions)
                //.Include(cr=>cr.CookbookRecipes).ThenInclude(c=>c.Cookbook)
                .Include(u => u.User)
                .Include(c => c.Category)
                .Include(p =>p.Photos) //ODKOMENTOWAC
                //.Include(ri => ri.RecipeIngredients)
                .Include(ing => ing.RecipeIngredients).ThenInclude(r => r.Ingredient)
                .Include(ing => ing.RecipeIngredients).ThenInclude(m => m.Measure)
                .FirstOrDefaultAsync(u => u.Id == recipeId);

            return recipe;
        }

        public void AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe));
            }

            _context.Recipes.Add(recipe);

        }
        //public void AddRecipeIngredient(RecipeIngredient recipeIngredient)
        //{
        //    if (recipeIngredient == null)
        //    {
        //        throw new ArgumentNullException(nameof(recipeIngredient));
        //    }

        //    _context.RecipeIngredients.Add(recipeIngredient);
        //}
        //public void UpdateRecipeIngredient(RecipeIngredient recipeIngredient)
        //{
        //    _context.Entry(recipeIngredient).State = EntityState.Modified;
        //}

        //public void DeleteRecipeIngredient(RecipeIngredient recipeIngredient)
        //{            
        //    _context.RecipeIngredients.Remove(recipeIngredient);
        //}

        #endregion

        #region User
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await _context.Users
                .Include(r=>r.Recipes)
                //.Include(c=>c.Cookbook)
                //.Include(c=>c.Cookbook)
                .ToListAsync<User>();

            var allCookbooks = await _context.Cookbooks.ToListAsync();

            foreach (var user in users)
            {
                if (user.Cookbook != null)
                {
                    user.Cookbook = allCookbooks.FirstOrDefault(c => c.Id == user.Cookbook.Id);
                }

            }

            return users;
        }

        public async Task<User> GetUserAsync(int userId)
        {
            var user = await _context.Users
                .Include(c=>c.Cookbook)
                .Include(r=>r.Recipes)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async Task<User> GetUserAsync(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
            return user;//to rzuca wyjatek jeśli znajdzie wiecej elementów, jesli tylko 1 to spuer. Tym sie rozni od FirstOrDefault 
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

        public async Task<IEnumerable<User>> GetUsersAsync(IEnumerable<int> userIds)
        {
            if (userIds == null)
            {
                throw new ArgumentNullException(nameof(userIds));
            }

            var users = await _context.Users.Where(u => userIds.Contains(u.Id)).OrderBy(u => u.LastName).ThenBy(u => u.FirstName).ToListAsync();
            return users;
        }         

        public void UpdateUser(User user)
        {
            // no code in this implementation ?
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            var isExist =await _context.Users.AnyAsync(u => u.Id == userId);
            return isExist;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username.ToLower());
        }
        #endregion

        #region Ingredient
        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            var ingredients=await _context.Ingredients.ToListAsync();
            return ingredients;
        }

        public async Task<bool> IngredientExistsAsync(int ingredientId)
        {
            var isExist =await _context.Ingredients.AnyAsync(u => u.Id == ingredientId);
            return isExist;
        }

        public async Task<Ingredient> GetIngredientAsync(int ingredientId)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(u => u.Id == ingredientId);
            return ingredient;
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
        public async Task<IEnumerable<Measure>> GetMeasuresAsync()
        {
            var measures = await _context.Measures.ToListAsync();
            return measures;
        }

        public async Task<bool> MeasureExistsAsync(int measureId)
        {
            var isExist = await _context.Measures.AnyAsync(u => u.Id == measureId);
            return isExist;
        }

        public async Task<Measure> GetMeasureAsync(int measureId)
        {
            var measure = await _context.Measures.FirstOrDefaultAsync(u => u.Id == measureId);
            return measure;
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

        public async Task<IEnumerable<Instruction>> GetInstructionsAsync()
        {
            var instructions=await _context.Instructions.ToListAsync();
            return instructions;
        }

        //public async Task<IEnumerable<Instruction>> GetRecipeInstructionsAsync(int recipeId)
        //{
        //    var recipeInstructions = await _context.Instructions.Where(r=>r.Id == recipeId).ToListAsync();
        //    return recipeInstructions;
        //}

        public async Task<bool> InstructionExistsAsync(int instructionId)
        {
            var isExist=await _context.Instructions.AnyAsync(u => u.Id == instructionId);
            return isExist;
        }

        public async Task<Instruction> GetInstructionAsync(int instructionId)
        {
            var instruction=await _context.Instructions.FirstOrDefaultAsync(u => u.Id == instructionId);
            return instruction;
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

        //public void UpdateInstruction(Instruction instruction)
        //{
        //    _context.Entry(instruction).State = EntityState.Modified;
        //    //_context.Recipes.Update(recipe);            
        //}
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

        public async Task<IEnumerable<Cookbook>> GetCookbooksAsync()
        {
            var cookbooks = await _context.Cookbooks
                .Include(c => c.CookbookRecipes).ThenInclude(r => r.Recipe).ThenInclude(p => p.Photos)
                .ToListAsync();
            return cookbooks;
        }

        public async Task <bool> CookbookExistsAsync(int cookbookId)
        {
            var isExist = await _context.Cookbooks.AnyAsync(u => u.Id == cookbookId);
            return isExist;
        }

        public async Task<Cookbook> GetCookbookAsync(int cookbookId)
        {
            var cookbook = await _context.Cookbooks
                .Include(c=>c.CookbookRecipes).ThenInclude(r=>r.Recipe).ThenInclude(p=>p.Photos)                     
                .FirstOrDefaultAsync(u => u.Id == cookbookId);

            //.ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
            return cookbook;
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

        public async Task<Cookbook> GetUserCookbookAsync(int userId)
        {
            var cookbook = await _context.Cookbooks
                .Include(c => c.CookbookRecipes).ThenInclude(r => r.Recipe).ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            return cookbook;
        } 
        #endregion

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();               
                        
            return categories;
        }
                
    }
}
