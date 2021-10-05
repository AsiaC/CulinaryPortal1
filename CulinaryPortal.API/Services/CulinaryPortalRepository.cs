using CulinaryPortal.API.DbContexts;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
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

        //TODO to jest to USUNIECIA bo nie potrzebne
        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0; //wiecej niz 0 zmian zostało zapisanych w bazie. Jesli coś zostanie zapisane w bazie to wartość bedzie wieksza niz 0
        }

        #region Recipe
        
        public async Task DeleteRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task<Recipe> GetUserRecipeAsync(int userId, int recipeId)
        {
            return await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId && r.UserId == userId);
        }

        public async Task<IEnumerable<Recipe>> GetUserRecipesAsync(int userId)
        {
            var userRecipes = await _context.Recipes.Where(r => r.UserId == userId)
                .Include(i => i.Instructions)
                .Include(p => p.Photos) 
                .Include(ing => ing.RecipeIngredients).ThenInclude(r => r.Ingredient)
                .Include(ing => ing.RecipeIngredients).ThenInclude(m => m.Measure)
                .Include(c => c.Category)
                .ToListAsync();
            return userRecipes;
        }

        public async Task<IEnumerable<Recipe>> GetRecipesAsync()
        {
            var recipes = await _context.Recipes
                .Include(i => i.Instructions)
                .Include(p => p.Photos) //ODKOMENTOWAĆ
                //.Include(cr => cr.CookbookRecipes).ThenInclude(c => c.Cookbook)
                .Include(ing=> ing.RecipeIngredients).ThenInclude(r=>r.Ingredient)
                .Include(ing => ing.RecipeIngredients).ThenInclude(m => m.Measure)
                .Include(c => c.Category)
                .Include(r => r.Rates)
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
                .Include(rate => rate.Rates)
                .FirstOrDefaultAsync(u => u.Id == recipeId);
            
            return recipe;
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe));
            }

            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<Recipe>> SearchRecipesAsync(SearchRecipeDto searchRecipeDto)
        {
            IEnumerable<Recipe> query = await _context.Recipes
                .Include(i => i.Instructions)
                .Include(p => p.Photos) 
                                        //.Include(cr => cr.CookbookRecipes).ThenInclude(c => c.Cookbook)
                .Include(ing => ing.RecipeIngredients).ThenInclude(r => r.Ingredient)
                .Include(ing => ing.RecipeIngredients).ThenInclude(m => m.Measure)
                .Include(c => c.Category)
                .ToListAsync();

            if (searchRecipeDto.UserId != null)
            {
                query = query.Where(r => r.UserId == searchRecipeDto.UserId);
            }

            if (searchRecipeDto.CategoryId != null)
            {
                query = query.Where(r => r.CategoryId == searchRecipeDto.CategoryId);
            }

            if (searchRecipeDto.PreparationTimeId != null)
            {
                query = query.Where(r => (int)r.PreparationTime <= searchRecipeDto.PreparationTimeId);
            }

            if (searchRecipeDto.DifficultyLevelId != null)
            {
                query = query.Where(r => (int)r.DifficultyLevel == searchRecipeDto.DifficultyLevelId);
            }

            if (!String.IsNullOrWhiteSpace(searchRecipeDto.Name)) 
            {
                query = query.Where(r => r.Name == searchRecipeDto.Name);
            }           
            return query;
        }

        public async Task<IEnumerable<Photo>> GetRecipePhotosAsync(int recipeId)
        {
            return await _context.Photos.Where(p => p.RecipeId == recipeId).ToListAsync();            
        }

        public async Task<int> CountAssociatedCookbooksAsync(int recipeId) 
        {
            return await _context.Cookbooks.SelectMany(x => x.CookbookRecipes.Where(a => a.RecipeId == recipeId)).CountAsync();
        }
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
            return await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
            //to rzuca wyjatek jeśli znajdzie wiecej elementów, jesli tylko 1 to spr. Tym sie rozni od FirstOrDefault 
        }

        public async Task AddUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
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

        public async Task UpdateUserAsync(User user)//TODO togo mozna sie pozbyc - przykład jak recipe czy shoppinglist
        {
            // no code in this implementation ?
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<Rate> GetUserRecipeRateAsync(int userId, int recipeId) 
        {
            return await _context.Rates.FirstOrDefaultAsync(r => r.RecipeId == recipeId && r.UserId == userId);             
        }
        #endregion

        #region Ingredient
        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            return await _context.Ingredients.ToListAsync();
        }

        public async Task<Ingredient> GetIngredientAsync(int ingredientId)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(u => u.Id == ingredientId);             
        }

        public async Task AddIngredientAsync(Ingredient ingredient)
        {
            if (ingredient == null)
            {
                throw new ArgumentNullException(nameof(ingredient));
            }

            await _context.Ingredients.AddAsync(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIngredientAsync(Ingredient ingredient)
        {
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Measure
        public async Task<IEnumerable<Measure>> GetMeasuresAsync()
        {
            return await _context.Measures.ToListAsync();
        }        

        public async Task<Measure> GetMeasureAsync(int measureId)
        {
            return await _context.Measures.FirstOrDefaultAsync(u => u.Id == measureId);
        }

        public async Task AddMeasureAsync(Measure measure)
        {
            if (measure == null)
            {
                throw new ArgumentNullException(nameof(measure));
            }

            await _context.Measures.AddAsync(measure);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMeasureAsync(Measure measure)
        {
            _context.Measures.Remove(measure);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Instruction

        public async Task<IEnumerable<Instruction>> GetInstructionsAsync()
        {
            return await _context.Instructions.ToListAsync();
        }        

        public async Task<Instruction> GetInstructionAsync(int instructionId)
        {
            return await _context.Instructions.FirstOrDefaultAsync(u => u.Id == instructionId);
        }

        public async Task AddInstructionAsync(Instruction instruction)
        {
            if (instruction == null)
            {
                throw new ArgumentNullException(nameof(instruction));
            }

            await _context.Instructions.AddAsync(instruction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInstructionAsync(Instruction instruction)
        {
            _context.Instructions.Remove(instruction);
            await _context.SaveChangesAsync();
        }        
        #endregion

        #region ShoppingList

        public async Task<IEnumerable<ShoppingList>> GetShoppingListsAsync()
        {
            var shoppingLists = await _context.ShoppingLists
                .Include(l => l.Items)
                .Include(u => u.User)
                .ToListAsync();
            return shoppingLists;
        }

        public async Task<ShoppingList> GetShoppingListAsync(int shoppingListId)
        {
            var shoppingList = await _context.ShoppingLists
                .Include(l => l.Items)
                .FirstOrDefaultAsync(u => u.Id == shoppingListId);
            return shoppingList;
        }

        public async Task AddShoppingListAsync(ShoppingList shoppingList)
        {
            if (shoppingList == null)
            {
                throw new ArgumentNullException(nameof(shoppingList));
            }

            await _context.ShoppingLists.AddAsync(shoppingList);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShoppingListAsync(ShoppingList shoppingList)
        {
            _context.ShoppingLists.Remove(shoppingList);
            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<ShoppingList>> GetUserShoppingListsAsync(int userId)
        {
            var userShoppingLists = await _context.ShoppingLists.Where(r => r.UserId == userId)
                .Include(l => l.Items)                
                .ToListAsync();
            return userShoppingLists;
        }
        #endregion

        #region Cookbook

        public async Task<IEnumerable<Cookbook>> GetCookbooksAsync()
        {
            var cookbooks = await _context.Cookbooks
                .Include(c => c.CookbookRecipes).ThenInclude(r => r.Recipe).ThenInclude(p => p.Photos)
                .Include(c => c.User)
                .ToListAsync();
            return cookbooks;
        }

        public async Task<Cookbook> GetCookbookAsync(int cookbookId)
        {
            var cookbook = await _context.Cookbooks
                .Include(c=>c.CookbookRecipes).ThenInclude(r=>r.Recipe).ThenInclude(p=>p.Photos)                    
                .FirstOrDefaultAsync(u => u.Id == cookbookId);
            return cookbook;
        }

        public async Task AddCookbookAsync(Cookbook cookbook)
        {
            if (cookbook == null)
            {
                throw new ArgumentNullException(nameof(cookbook));
            }

            await _context.Cookbooks.AddAsync(cookbook);
            await _context.SaveChangesAsync();            
        }

        public async Task DeleteCookbookAsync(Cookbook cookbook)
        {
            _context.Cookbooks.Remove(cookbook);
            await _context.SaveChangesAsync();
        }

        public async Task<Cookbook> GetUserCookbookAsync(int userId)
        {
            var cookbook = await _context.Cookbooks
                .Include(c => c.CookbookRecipes).ThenInclude(r => r.Recipe).ThenInclude(c=>c.Category)
                .Include(c => c.CookbookRecipes).ThenInclude(r => r.Recipe).ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            return cookbook;
        }

        public async Task<IEnumerable<Recipe>> SearchUserCookbookRecipesAsync(SearchRecipeDto searchRecipeDto)
        {
            var cookbook = await _context.Cookbooks
                .Include(c => c.CookbookRecipes).ThenInclude(r => r.Recipe).ThenInclude(c => c.Category)
                .Include(c => c.CookbookRecipes).ThenInclude(r => r.Recipe).ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(u => u.UserId == searchRecipeDto.UserId);

            IEnumerable<Recipe> query = cookbook.CookbookRecipes.Select(r => r.Recipe);

            if (searchRecipeDto.CategoryId != null)
            {
                query = query.Where(r => r.CategoryId == searchRecipeDto.CategoryId);
            }

            if (searchRecipeDto.PreparationTimeId != null)
            {
                query = query.Where(r => (int)r.PreparationTime <= searchRecipeDto.PreparationTimeId);
            }

            if (searchRecipeDto.DifficultyLevelId != null)
            {
                query = query.Where(r => (int)r.DifficultyLevel == searchRecipeDto.DifficultyLevelId);
            }

            if (!String.IsNullOrWhiteSpace(searchRecipeDto.Name))
            {
                query = query.Where(r => r.Name == searchRecipeDto.Name);
            }
            return query;
        }
        #endregion

        #region Photo
        public async Task AddPhotoAsync(Photo photo)
        {
            if (photo == null)
            {
                throw new ArgumentNullException(nameof(photo));
            }

            await _context.Photos.AddAsync(photo);
            await _context.SaveChangesAsync();
        }
        public async Task<Photo> GetPhotoAsync(int photoId)
        {
            return await _context.Photos.FirstOrDefaultAsync(u => u.Id == photoId);
        }

        public async Task DeletePhotoAsync(Photo photo)
        {
            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Rate
        public async Task<IEnumerable<Rate>> GetRatesAsync()
        {
            var rates = await _context.Rates                
                .ToListAsync();          

            return rates;
        }

        public async Task<Rate> GetRateAsync(int rateId)
        {
           var rate = await _context.Rates.FirstOrDefaultAsync(u => u.Id == rateId);
            return rate;
        }

        public async Task AddRateAsync(Rate rate)
        {
            if (rate == null)
            {
                throw new ArgumentNullException(nameof(rate));
            }

            await _context.Rates.AddAsync(rate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRateAsync(Rate rate)
        {
            _context.Rates.Remove(rate);
            await _context.SaveChangesAsync();
        }

        #endregion
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();  
        }
                
    }
}
