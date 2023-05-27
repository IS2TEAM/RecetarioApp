using System;
using System.Collections.Generic;

namespace RecetarioApp.Models;

public partial class IngredientUser
{
    public int IdIngredient { get; set; }

    public string NameIngredient { get; set; } = null!;
    public string ShopIngredient { get; set; } = null!;

    public int PriceIngredient { get; set; }

}
