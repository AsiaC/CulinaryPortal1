using CulinaryPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Persistence
{
    public class CulinaryPortalDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>    //: DbContext
    {
        public CulinaryPortalDbContext(DbContextOptions<CulinaryPortalDbContext>options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CookbookRecipe>()
                .HasKey(cr => new { cr.CookbookId, cr.RecipeId });

            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.MeasureId, ri.RecipeId, ri.IngredientId });

            modelBuilder.Entity<User>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            modelBuilder.Entity<Role>()
               .HasMany(ur => ur.UserRoles)
               .WithOne(u => u.Role)
               .HasForeignKey(ur => ur.RoleId)
               .IsRequired();

            modelBuilder.Entity<UserRole>()
                .HasKey(aur => new { aur.RoleId, aur.UserId });

            modelBuilder.Entity<Category>()
                .HasData(
                    new Category { Id = 1, Name = "Breakfast" },
                    new Category { Id = 2, Name = "Desserts" },
                    new Category { Id = 3, Name = "Dinner" },
                    new Category { Id = 4, Name = "Drinks" },
                    new Category { Id = 5, Name = "Other" },
                    new Category { Id = 6, Name = "Salads" },
                    new Category { Id = 7, Name = "Snacks" },
                    new Category { Id = 8, Name = "Soups" }
                );

            modelBuilder.Entity<Measure>()
                .HasData(
                    new Measure { Id = 1, Name = "clove" }, 
                    new Measure { Id = 2, Name = "cup" },
                    new Measure { Id = 3, Name = "gram" },
                    new Measure { Id = 4, Name = "piece" },
                    new Measure { Id = 5, Name = "pinch" },
                    new Measure { Id = 6, Name = "tablespoon" },
                    new Measure { Id = 7, Name = "teaspoon" }
                    ); ;

            modelBuilder.Entity<Ingredient>()
                .HasData(
                    new Ingredient { Id = 1, Name = "apple" },                 
                    new Ingredient { Id = 2, Name = "baking powder" },
                    new Ingredient { Id = 3, Name = "banana" },
                    new Ingredient { Id = 4, Name = "black pepper" },
                    new Ingredient { Id = 5, Name = "butter" },
                    new Ingredient { Id = 6, Name = "cabbage" },
                    new Ingredient { Id = 7, Name = "carrot" },
                    new Ingredient { Id = 8, Name = "cheese" },
                    new Ingredient { Id = 9, Name = "chicken stock" },
                    new Ingredient { Id = 10, Name = "chive" },
                    new Ingredient { Id = 11, Name = "coconut oil" },
                    new Ingredient { Id = 12, Name = "coriander seeds" },
                    new Ingredient { Id = 13, Name = "cucumber" },
                    new Ingredient { Id = 14, Name = "cumin seeds" },
                    new Ingredient { Id = 15, Name = "egg" },
                    new Ingredient { Id = 16, Name = "fat-free milk" },
                    new Ingredient { Id = 17, Name = "fennel seeds" },
                    new Ingredient { Id = 18, Name = "flour" },
                    new Ingredient { Id = 19, Name = "garlic" },
                    new Ingredient { Id = 20, Name = "garlic salt" },
                    new Ingredient { Id = 21, Name = "green cabbage" },
                    new Ingredient { Id = 22, Name = "ground cinnamon" },
                    new Ingredient { Id = 23, Name = "honey" },
                    new Ingredient { Id = 24, Name = "jalapeno pepper" },
                    new Ingredient { Id = 25, Name = "lemon" },
                    new Ingredient { Id = 26, Name = "low-fat yogut" },
                    new Ingredient { Id = 27, Name = "mayonnaise" },
                    new Ingredient { Id = 28, Name = "milk" },    
                    new Ingredient { Id = 29, Name = "mustard" },
                    new Ingredient { Id = 30, Name = "oats" },      
                    new Ingredient { Id = 31, Name = "oil" },
                    new Ingredient { Id = 32, Name = "olive oil" },
                    new Ingredient { Id = 33, Name = "onion" },
                    new Ingredient { Id = 34, Name = "parmesan" },
                    new Ingredient { Id = 35, Name = "pepper" },
                    new Ingredient { Id = 36, Name = "potato" },
                    new Ingredient { Id = 37, Name = "rainbow trout fillet" },
                    new Ingredient { Id = 38, Name = "red cabbage" },
                    new Ingredient { Id = 39, Name = "salt" },
                    new Ingredient { Id = 40, Name = "sour cream" },
                    new Ingredient { Id = 41, Name = "strawberries" },
                    new Ingredient { Id = 42, Name = "sugar" },
                    new Ingredient { Id = 43, Name = "tomato" },
                    new Ingredient { Id = 44, Name = "vanilla extract" },
                    new Ingredient { Id = 45, Name = "water" },
                    new Ingredient { Id = 46, Name = "white-wine vinegar" }
                );
        }

        public DbSet<Cookbook> Cookbooks { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        //public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Recipe> Recipes { get; set; }       
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<Category> Categories { get; set; }        
        public DbSet<Rate> Rates { get; set; }
    }
}
