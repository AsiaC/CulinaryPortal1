﻿// <auto-generated />
using System;
using CulinaryPortal.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CulinaryPortal.Persistence.Migrations
{
    [DbContext(typeof(CulinaryPortalDbContext))]
    partial class CulinaryPortalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Breakfast"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Desserts"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Dinner"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Drinks"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Other"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Salads"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Snacks"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Soups"
                        });
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Cookbook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Cookbooks");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.CookbookRecipe", b =>
                {
                    b.Property<int>("CookbookId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("CookbookId", "RecipeId");

                    b.HasIndex("RecipeId");

                    b.ToTable("CookbookRecipe");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "apple"
                        },
                        new
                        {
                            Id = 2,
                            Name = "baking powder"
                        },
                        new
                        {
                            Id = 3,
                            Name = "banana"
                        },
                        new
                        {
                            Id = 4,
                            Name = "black pepper"
                        },
                        new
                        {
                            Id = 5,
                            Name = "butter"
                        },
                        new
                        {
                            Id = 6,
                            Name = "cabbage"
                        },
                        new
                        {
                            Id = 7,
                            Name = "carrot"
                        },
                        new
                        {
                            Id = 8,
                            Name = "cheese"
                        },
                        new
                        {
                            Id = 9,
                            Name = "chicken stock"
                        },
                        new
                        {
                            Id = 10,
                            Name = "chive"
                        },
                        new
                        {
                            Id = 11,
                            Name = "coconut oil"
                        },
                        new
                        {
                            Id = 12,
                            Name = "coriander seeds"
                        },
                        new
                        {
                            Id = 13,
                            Name = "cucumber"
                        },
                        new
                        {
                            Id = 14,
                            Name = "cumin seeds"
                        },
                        new
                        {
                            Id = 15,
                            Name = "egg"
                        },
                        new
                        {
                            Id = 16,
                            Name = "fat-free milk"
                        },
                        new
                        {
                            Id = 17,
                            Name = "fennel seeds"
                        },
                        new
                        {
                            Id = 18,
                            Name = "flour"
                        },
                        new
                        {
                            Id = 19,
                            Name = "garlic"
                        },
                        new
                        {
                            Id = 20,
                            Name = "garlic salt"
                        },
                        new
                        {
                            Id = 21,
                            Name = "green cabbage"
                        },
                        new
                        {
                            Id = 22,
                            Name = "ground cinnamon"
                        },
                        new
                        {
                            Id = 23,
                            Name = "honey"
                        },
                        new
                        {
                            Id = 24,
                            Name = "jalapeno pepper"
                        },
                        new
                        {
                            Id = 25,
                            Name = "lemon"
                        },
                        new
                        {
                            Id = 26,
                            Name = "low-fat yogut"
                        },
                        new
                        {
                            Id = 27,
                            Name = "mayonnaise"
                        },
                        new
                        {
                            Id = 28,
                            Name = "milk"
                        },
                        new
                        {
                            Id = 29,
                            Name = "mustard"
                        },
                        new
                        {
                            Id = 30,
                            Name = "oats"
                        },
                        new
                        {
                            Id = 31,
                            Name = "oil"
                        },
                        new
                        {
                            Id = 32,
                            Name = "olive oil"
                        },
                        new
                        {
                            Id = 33,
                            Name = "onion"
                        },
                        new
                        {
                            Id = 34,
                            Name = "parmesan"
                        },
                        new
                        {
                            Id = 35,
                            Name = "pepper"
                        },
                        new
                        {
                            Id = 36,
                            Name = "potato"
                        },
                        new
                        {
                            Id = 37,
                            Name = "rainbow trout fillet"
                        },
                        new
                        {
                            Id = 38,
                            Name = "red cabbage"
                        },
                        new
                        {
                            Id = 39,
                            Name = "salt"
                        },
                        new
                        {
                            Id = 40,
                            Name = "sour cream"
                        },
                        new
                        {
                            Id = 41,
                            Name = "strawberries"
                        },
                        new
                        {
                            Id = 42,
                            Name = "sugar"
                        },
                        new
                        {
                            Id = 43,
                            Name = "tomato"
                        },
                        new
                        {
                            Id = 44,
                            Name = "vanilla extract"
                        },
                        new
                        {
                            Id = 45,
                            Name = "water"
                        },
                        new
                        {
                            Id = 46,
                            Name = "white-wine vinegar"
                        });
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Instruction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("Step")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Instruction");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.ListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("ShoppingListId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingListId");

                    b.ToTable("ListItem");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Measure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Measures");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "clove"
                        },
                        new
                        {
                            Id = 2,
                            Name = "cup"
                        },
                        new
                        {
                            Id = 3,
                            Name = "gram"
                        },
                        new
                        {
                            Id = 4,
                            Name = "piece"
                        },
                        new
                        {
                            Id = 5,
                            Name = "pinch"
                        },
                        new
                        {
                            Id = 6,
                            Name = "tablespoon"
                        },
                        new
                        {
                            Id = 7,
                            Name = "teaspoon"
                        });
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("ContentPhoto")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Rate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("DifficultyLevel")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("PreparationTime")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.RecipeIngredient", b =>
                {
                    b.Property<int>("MeasureId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("MeasureId", "RecipeId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeIngredient");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.ShoppingList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ShoppingLists");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Cookbook", b =>
                {
                    b.HasOne("CulinaryPortal.Domain.Entities.User", "User")
                        .WithOne("Cookbook")
                        .HasForeignKey("CulinaryPortal.Domain.Entities.Cookbook", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.CookbookRecipe", b =>
                {
                    b.HasOne("CulinaryPortal.Domain.Entities.Cookbook", null)
                        .WithMany("CookbookRecipes")
                        .HasForeignKey("CookbookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CulinaryPortal.Domain.Entities.Recipe", "Recipe")
                        .WithMany("CookbookRecipes")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Instruction", b =>
                {
                    b.HasOne("CulinaryPortal.Domain.Entities.Recipe", "Recipe")
                        .WithMany("Instructions")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.ListItem", b =>
                {
                    b.HasOne("CulinaryPortal.Domain.Entities.ShoppingList", "ShoppingList")
                        .WithMany("Items")
                        .HasForeignKey("ShoppingListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingList");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Photo", b =>
                {
                    b.HasOne("CulinaryPortal.Domain.Entities.Recipe", "Recipe")
                        .WithMany("Photos")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Rate", b =>
                {
                    b.HasOne("CulinaryPortal.Domain.Entities.Recipe", "Recipe")
                        .WithMany("Rates")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CulinaryPortal.Domain.Entities.User", "User")
                        .WithMany("Rates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Recipe", b =>
                {
                    b.HasOne("CulinaryPortal.Domain.Entities.Category", "Category")
                        .WithMany("Recipes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CulinaryPortal.Domain.Entities.User", "User")
                        .WithMany("Recipes")
                        .HasForeignKey("UserId");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.RecipeIngredient", b =>
                {
                    b.HasOne("CulinaryPortal.Domain.Entities.Ingredient", "Ingredient")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CulinaryPortal.Domain.Entities.Measure", "Measure")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("MeasureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CulinaryPortal.Domain.Entities.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Measure");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.ShoppingList", b =>
                {
                    b.HasOne("CulinaryPortal.Domain.Entities.User", "User")
                        .WithMany("ShoppingLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("CulinaryPortal.Domain.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CulinaryPortal.Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("CulinaryPortal.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("CulinaryPortal.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("CulinaryPortal.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("CulinaryPortal.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Category", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Cookbook", b =>
                {
                    b.Navigation("CookbookRecipes");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Ingredient", b =>
                {
                    b.Navigation("RecipeIngredients");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Measure", b =>
                {
                    b.Navigation("RecipeIngredients");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Recipe", b =>
                {
                    b.Navigation("CookbookRecipes");

                    b.Navigation("Instructions");

                    b.Navigation("Photos");

                    b.Navigation("Rates");

                    b.Navigation("RecipeIngredients");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.ShoppingList", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("CulinaryPortal.Domain.Entities.User", b =>
                {
                    b.Navigation("Cookbook");

                    b.Navigation("Rates");

                    b.Navigation("Recipes");

                    b.Navigation("ShoppingLists");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
