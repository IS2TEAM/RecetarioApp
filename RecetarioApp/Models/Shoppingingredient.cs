using System;
using System.Collections.Generic;

namespace RecetarioApp.Models;

public partial class Shoppingingredient
{
    public int IdShoppingIngredient { get; set; }

    public int IdList { get; set; }

    public int IdIngredient { get; set; }

    public virtual Ingredient IdIngredientNavigation { get; set; } = null!;

    public virtual Shoppinglist IdListNavigation { get; set; } = null!;
}
