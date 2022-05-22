using CulinaryPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Persistence
{
    public interface IRateRepository : IAsyncRepository<Rate>
    {
        Task<Rate> GetRecipeRateAsync(int userId, int recipeId);
    }
}
