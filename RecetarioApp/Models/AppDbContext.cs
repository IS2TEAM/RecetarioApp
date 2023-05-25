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

    public virtual DbSet<IngredienteTiendum> IngredienteTienda { get; set; }

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
            entity.HasKey(e => e.IdIngredient).HasName("PK__ingredie__9D79738D7A611B55");

            entity.ToTable("ingredients");

            entity.Property(e => e.IdIngredient).HasColumnName("id_ingredient");
            entity.Property(e => e.NameIngredient)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name_ingredient");
        });

        modelBuilder.Entity<IngredienteTiendum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ingrediente_tienda");

            entity.Property(e => e.IdIngrediente).HasColumnName("id_ingrediente");
            entity.Property(e => e.IdTienda).HasColumnName("id_tienda");
            entity.Property(e => e.Precio).HasColumnName("precio");

            entity.HasOne(d => d.IdIngredienteNavigation).WithMany()
                .HasForeignKey(d => d.IdIngrediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ingredien__id_in__5EBF139D");

            entity.HasOne(d => d.IdTiendaNavigation).WithMany()
                .HasForeignKey(d => d.IdTienda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ingredien__id_ti__5DCAEF64");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.IdRecipe).HasName("PK__recipes__1F2843E6A072B33C");

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
            entity
                .HasNoKey()
                .ToTable("recipesingredients");

            entity.Property(e => e.IdIngredient).HasColumnName("id_ingredient");
            entity.Property(e => e.IdRecipe).HasColumnName("id_recipe");
            entity.Property(e => e.Quantity)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("quantity");

            entity.HasOne(d => d.IdIngredientNavigation).WithMany()
                .HasForeignKey(d => d.IdIngredient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_recipesingredients_ingredients");

            entity.HasOne(d => d.IdRecipeNavigation).WithMany()
                .HasForeignKey(d => d.IdRecipe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_recipesingredients_recipes");
        });

        modelBuilder.Entity<Shoppingingredient>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("shoppingingredients");

            entity.Property(e => e.IdIngredient).HasColumnName("id_ingredient");
            entity.Property(e => e.IdList).HasColumnName("id_list");

            entity.HasOne(d => d.IdIngredientNavigation).WithMany()
                .HasForeignKey(d => d.IdIngredient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shoppingingredients_ingredients");

            entity.HasOne(d => d.IdListNavigation).WithMany()
                .HasForeignKey(d => d.IdList)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_shoppingingredients_shoppinglist");
        });

        modelBuilder.Entity<Shoppinglist>(entity =>
        {
            entity.HasKey(e => e.IdList).HasName("PK__shopping__99804307CCBFE5F4");

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
            entity.HasKey(e => e.IdTienda).HasName("PK__tienda__7C49D73634F2D987");

            entity.ToTable("tienda");

            entity.Property(e => e.IdTienda).HasColumnName("id_tienda");
            entity.Property(e => e.NombreTienda)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre_tienda");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__users__D2D14637A8C0DCDA");

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
