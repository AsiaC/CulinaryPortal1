using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryPortal.Domain.Entities;

namespace CulinaryPortal.Application.Persistence
{
    public interface IPhotoRepository : IAsyncRepository<Photo>
    {
        Task<List<Photo>> GetRecipePhotosAsync(int recipeId);
        Task AddPhotoAsync(Photo photo);
    }
}
