using System;
using System.Collections.Generic;

namespace RecetarioApp.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Username { get; set; } = null!;

    public string EmailUser { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
