using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RecetarioApp.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<IngredientTiendum> IngredientTiendum { get; set; }

    public virtual DbSet<IngredientsUser> IngredientsUsers { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Recipesingredient> Recipesingredients { get; set; }

    public virtual DbSet<Shoppingingredient> Shoppingingredients { get; set; }

    public virtual DbSet<Shoppinglist> Shoppinglists { get; set; }

    public virtual DbSet<Tiendum> Tienda { get; set; }

    public virtual DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IdIngredient).HasName("PK__ingredie__9D79738DE45DA379");

            entity.ToTable("ingredients");

            entity.Property(e => e.IdIngredient).HasColumnName("id_ingredient");
            entity.Property(e => e.NameIngredient)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name_ingredient");
        });

        modelBuilder.Entity<IngredientTiendum>(entity =>
        {
            entity.HasKey(e => e.IdIngredientTiendum).HasName("PK__ingredie__3B99A359C4B94B76");

            entity.ToTable("ingredientTiendum");

            entity.Property(e => e.IdIngredientTiendum).HasColumnName("id_ingredient_tiendum");
            entity.Property(e => e.IdIngredient).HasColumnName("id_ingredient");
            entity.Property(e => e.IdTiendum).HasColumnName("id_tiendum");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.IdIngredientNavigation).WithMany(p => p.IngredientTiendum)
                .HasForeignKey(d => d.IdIngredient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ingredien__id_in__46E78A0C");

            entity.HasOne(d => d.IdTiendumNavigation).WithMany(p => p.IngredientTiendum)
                .HasForeignKey(d => d.IdTiendum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ingredien__id_ti__47DBAE45");
        });

        modelBuilder.Entity<IngredientsUser>(entity =>
        {
            entity.HasKey(e => e.IdIngredient).HasName("PK__ingredie__9D79738D4E64BC8F");

            entity.ToTable("ingredientsUsers");

            entity.Property(e => e.IdIngredient).HasColumnName("id_ingredient");
            entity.Property(e => e.NameIngredient)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name_ingredient");
            entity.Property(e => e.NameShop)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name_shop");
            entity.Property(e => e.PriceShop).HasColumnName("price_shop");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.IdRecipe).HasName("PK__recipes__1F2843E60818EB2D");

            entity.ToTable("recipes");

            entity.Property(e => e.IdRecipe).HasColumnName("id_recipe");
            entity.Property(e => e.Instructions)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("instructions");
            entity.Property(e => e.Portions).HasColumnName("portions");
            entity.Property(e => e.RecipePhoto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("recipe_photo");
            entity.Property(e => e.RecipesName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("recipes_name");
            entity.Property(e => e.TimePreparation).HasColumnName("timePreparation");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_recipes_users");
        });

        modelBuilder.Entity<Recipesingredient>(entity =>
        {
            entity.HasKey(e => e.IdRecipeIngredient).HasName("PK__recipesi__2B43ED794C4443FB");

            entity.ToTable("recipesingredients");

            entity.Property(e => e.IdRecipeIngredient).HasColumnName("id_recipe_ingredient");
            entity.Property(e => e.IdIngredient).HasColumnName("id_ingredient");
            entity.Property(e => e.IdRecipe).HasColumnName("id_recipe");
            entity.Property(e => e.Quantity)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("quantity");

            entity.HasOne(d => d.IdIngredientNavigation).WithMany(p => p.Recipesingredients)
                .HasForeignKey(d => d.IdIngredient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_recipesingredients_ingredients");

            entity.HasOne(d => d.IdRecipeNavigation).WithMany(p => p.Recipesingredients)
                .HasForeignKey(d => d.IdRecipe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_recipesingredients_recipes");
        });

        modelBuilder.Entity<Shoppingingredient>(entity =>
        {
            entity.HasKey(e => e.IdShoppingIngredients).HasName("PK__shopping__81FD008215E1DF81");

            entity.ToTable("shoppingingredients");

            entity.Property(e => e.IdShoppingIngredients).HasColumnName("id_shoppingIngredients");
            entity.Property(e => e.IdIngredient).HasColumnName("id_ingredient");
            entity.Property(e => e.IdList).HasColumnName("id_list");

            entity.HasOne(d => d.IdIngredientNavigation).WithMany(p => p.Shoppingingredients)
                .HasForeignKey(d => d.IdIngredient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shoppingingredients_ingredients");

            entity.HasOne(d => d.IdListNavigation).WithMany(p => p.Shoppingingredients)
                .HasForeignKey(d => d.IdList)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shoppingingredients_shoppinglist");
        });

        modelBuilder.Entity<Shoppinglist>(entity =>
        {
            entity.HasKey(e => e.IdList).HasName("PK__shopping__9980430701A2F7EF");

            entity.ToTable("shoppinglist");

            entity.Property(e => e.IdList)
                .ValueGeneratedNever()
                .HasColumnName("id_list");
            entity.Property(e => e.IdRecipe).HasColumnName("id_recipe");

            entity.HasOne(d => d.IdRecipeNavigation).WithMany(p => p.Shoppinglists)
                .HasForeignKey(d => d.IdRecipe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shoppinglist_recipes");
        });

        modelBuilder.Entity<Tiendum>(entity =>
        {
            entity.HasKey(e => e.IdTiendum).HasName("PK__tiendum__8B4AB0BFFCA09E32");

            entity.ToTable("tiendum");

            entity.Property(e => e.IdTiendum).HasColumnName("id_tiendum");
            entity.Property(e => e.NameTiendum)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name_tiendum");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__users__D2D1463721488889");

            entity.ToTable("users");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.EmailUser)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email_user");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
