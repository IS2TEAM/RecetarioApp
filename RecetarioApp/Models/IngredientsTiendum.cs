using System;
using System.Collections.Generic;

namespace RecetarioApp.Models;

public partial class IngredienteTiendum
{
    public int IdIngredientTiendum { get; set; }
    public int IdTienda { get; set; }

    public int IdIngrediente { get; set; }

    public int Precio { get; set; }

    public virtual Ingredient IdIngredienteNavigation { get; set; } = null!;

    public virtual Tiendum IdTiendaNavigation { get; set; } = null!;
}
