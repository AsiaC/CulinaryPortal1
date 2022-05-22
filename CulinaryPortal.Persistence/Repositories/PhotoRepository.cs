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
    public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(CulinaryPortalDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Photo>> GetRecipePhotosAsync(int recipeId)
        {
            return await _dbContext.Photos.Where(p => p.RecipeId == recipeId).ToListAsync();
        }

        public async Task AddPhotoAsync(Photo photo)
        {
            if (photo == null)
            {
                throw new ArgumentNullException(nameof(photo));
            }

            await _dbContext.Photos.AddAsync(photo);
            await _dbContext.SaveChangesAsync();
        }
    }
}
