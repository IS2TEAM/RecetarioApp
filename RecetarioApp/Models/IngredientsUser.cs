using System;
using System.Collections.Generic;

namespace RecetarioApp.Models;

public partial class IngredientsUser
{
    public int IdIngredient { get; set; }

    public string NameIngredient { get; set; } = null!;

    public string NameShop { get; set; } = null!;

    public int PriceShop { get; set; }
}
