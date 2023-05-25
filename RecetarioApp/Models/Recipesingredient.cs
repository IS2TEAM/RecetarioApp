using System;
using System.Collections.Generic;

namespace RecetarioApp.Models;

public partial class Recipesingredient
{
    public int IdIngredient { get; set; }

    public int IdRecipe { get; set; }

    public string Quantity { get; set; } = null!;

    public virtual Ingredient IdIngredientNavigation { get; set; } = null!;

    public virtual Recipe IdRecipeNavigation { get; set; } = null!;
}
