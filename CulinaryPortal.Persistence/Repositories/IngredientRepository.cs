using CulinaryPortal.Application.Persistence;
using CulinaryPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Persistence.Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(CulinaryPortalDbContext dbContext) : base(dbContext)
        {

        }
    }
}
