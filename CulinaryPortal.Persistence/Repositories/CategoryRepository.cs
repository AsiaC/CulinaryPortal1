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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CulinaryPortalDbContext dbContext) : base(dbContext)
        {
            
        }
        public async Task<List<Category>> GetCategoriesWithRecipesAsync()
        {
            return await _dbContext.Categories.Include(r => r.Recipes).ToListAsync();
        }
        
    }
}
