namespace RecetarioApp.Models.ModelDTO
{
    public class RecipeDTO
    {
        public int UserId { get; set; }

        public string RecipesName { get; set; } = null!;

        public string Instructions { get; set; } = null!;

        public int TimePreparation { get; set; }

        public int Portions { get; set; }

        public string RecipePhoto { get; set; } = null!;
    }
}
