using CulinaryPortal.Application.Persistence;
using CulinaryPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Persistence.Repositories
{
    public class CookbookRepository : BaseRepository<Cookbook>, ICookbookRepository
    {
        public CookbookRepository(CulinaryPortalDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Cookbook>> GetCookbooksWithRecipesAsync() //GetCookbooksAsync
        {
            var cookbooks = await _dbContext.Cookbooks
                .Include(c => c.CookbookRecipes).ThenInclude(r => r.Recipe).ThenInclude(p => p.Photos)
                .Include(c => c.User)
                .ToListAsync();

            return cookbooks;
        }

        public async Task<Cookbook> GetCookbookWithRecipesAsync(int cookbookId)//GetCookbookAsync
        {
            var cookbook = await _dbContext.Cookbooks
                .Include(c => c.CookbookRecipes).ThenInclude(r => r.Recipe).ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(u => u.Id == cookbookId);
            return cookbook;
        }
        //todo PUT
    }
}
