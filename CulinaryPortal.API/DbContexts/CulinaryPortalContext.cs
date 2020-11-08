using CulinaryPortal.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryPortal.API.DbContexts
{
    public class CulinaryPortalContext : DbContext
    {
        public CulinaryPortalContext(DbContextOptions<CulinaryPortalContext>options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CookbookRecipe>()
                .HasKey(cr => new { cr.CookbookId, cr.RecipeId });

            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.MeasureId, ri.RecipeId, ri.IngredientId });
        }

        public DbSet<Cookbook> Cookbooks { get; set; }
//        public DbSet<CookbookRecipe> CookbookRecipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
 //      public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
