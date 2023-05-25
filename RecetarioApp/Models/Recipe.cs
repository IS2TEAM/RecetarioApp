using System;
using System.Collections.Generic;

namespace RecetarioApp.Models;

public partial class Recipe
{
    public int IdRecipe { get; set; }

    public int UserId { get; set; }

    public string RecipesName { get; set; } = null!;

    public string Instructions { get; set; } = null!;

    public int TimePreparation { get; set; }

    public int Portions { get; set; }

    public string RecipePhoto { get; set; } = null!;

    public virtual ICollection<Shoppinglist> Shoppinglists { get; set; } = new List<Shoppinglist>();

    public virtual User User { get; set; } = null!;
}
