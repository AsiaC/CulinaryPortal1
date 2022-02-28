using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryPortal.Application.Persistence;
using CulinaryPortal.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CulinaryPortal.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CulinaryPortalDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CulinaryPortalConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICookbookRepository, CookbookRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IMeasureRepository, MeasureRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IRateRepository, RateRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
