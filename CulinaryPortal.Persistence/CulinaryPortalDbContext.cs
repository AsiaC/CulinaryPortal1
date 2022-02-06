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
    public class CulinaryPortalDbContext : IdentityDbContext<User, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>    //: DbContext
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

            modelBuilder.Entity<AppRole>()
               .HasMany(ur => ur.UserRoles)
               .WithOne(u => u.Role)
               .HasForeignKey(ur => ur.RoleId)
               .IsRequired();

            modelBuilder.Entity<AppUserRole>()
                .HasKey(aur => new { aur.RoleId, aur.UserId });
        }


        //nie dodaje encji, których nie chce wyświetlać jako odrebne elementy (jeśli zdjecie jest przypisane do użytkownika - uż moze miec wiele zdj - to nie ma sensu tu dodawać zdjecia bo jako osobny byt nigdy nie bedzie istniało zdjecie)
        public DbSet<Cookbook> Cookbooks { get; set; }
        //public DbSet<CookbookRecipe> CookbookRecipe { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        //      public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<Category> Categories { get; set; }
        //public DbSet<ListItem> Items { get; set; }
        public DbSet<Rate> Rates { get; set; }
    }
}
