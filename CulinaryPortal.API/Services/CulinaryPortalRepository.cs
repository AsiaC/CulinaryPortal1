using CulinaryPortal.API.DbContexts;
using CulinaryPortal.API.Entities;
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

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(user);
        }

        public void DeleteRecipe(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }

        public Recipe GetUserRecipe(int userId, int recipeId)
        {
            return _context.Recipes.FirstOrDefault(r => r.Id == recipeId && r.UserId == userId); 
        }

        public IEnumerable<Recipe> GetUserRecipes(int userId)
        {
            return _context.Recipes.Where(r => r.UserId == userId).ToList();
        }

        public User GetUser(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public IEnumerable<User> GetUsers(IEnumerable<int> userIds)
        {
            if (userIds == null)
            {
                throw new ArgumentNullException(nameof(userIds));
            }
            return _context.Users.Where(u => userIds.Contains(u.Id)).OrderBy(u => u.LastName).ThenBy(u => u.FirstName).ToList();
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList<User>();
        }       

        public void UpdateRecipe(Recipe recipe)
        {
            // no code in this implementation
        }

        public void UpdateUser(User user)
        {
            // no code in this implementation
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }
    }
}
