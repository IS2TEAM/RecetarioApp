using System;
using System.Collections.Generic;

namespace RecetarioApp.Models;

public partial class IngredientTiendum
{
    public int IdIngredientTiendum { get; set; }

    public int IdTiendum { get; set; }

    public int IdIngredient { get; set; }

    public int Price { get; set; }

    public virtual Ingredient IdIngredientNavigation { get; set; } = null!;

    public virtual Tiendum IdTiendumNavigation { get; set; } = null!;
}
