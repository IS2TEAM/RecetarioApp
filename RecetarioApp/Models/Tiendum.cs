using System;
using System.Collections.Generic;

namespace RecetarioApp.Models;

public partial class Tiendum
{
    public int IdTiendum { get; set; }

    public string NameTiendum { get; set; } = null!;

    public virtual ICollection<IngredientTiendum> IngredientTiendum { get; set; } = new List<IngredientTiendum>();
}
