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
    public class RateRepository : BaseRepository<Rate>, IRateRepository
    {
        public RateRepository(CulinaryPortalDbContext dbContext) : base(dbContext)
        {

        }        

        public async Task<Rate> GetRecipeRateAsync(int userId, int recipeId)
        {
            return await _dbContext.Rates.FirstOrDefaultAsync(r => r.RecipeId == recipeId && r.UserId == userId);
        }
    }
}
