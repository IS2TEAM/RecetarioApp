using System;
using System.Collections.Generic;

namespace RecetarioApp.Models;

public partial class Shoppinglist
{
    public int IdList { get; set; }

    public int IdRecipe { get; set; }

    public virtual Recipe IdRecipeNavigation { get; set; } = null!;
}
