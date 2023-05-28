using System;
using System.Collections.Generic;

namespace RecetarioApp.Models;

public partial class Ingredient
{
    public int IdIngredient { get; set; }

    public string NameIngredient { get; set; } = null!;

    public virtual ICollection<IngredientTiendum> IngredientTiendum { get; set; } = new List<IngredientTiendum>();

    public virtual ICollection<Recipesingredient> Recipesingredients { get; set; } = new List<Recipesingredient>();

    public virtual ICollection<Shoppingingredient> Shoppingingredients { get; set; } = new List<Shoppingingredient>();
}
