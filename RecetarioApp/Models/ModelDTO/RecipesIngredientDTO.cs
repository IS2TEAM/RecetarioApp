namespace RecetarioApp.Models.ModelDTO
{
    public class RecipesIngredientDTO
    {
        public int IdRecipeIngredient { get; set; }

        public int IdIngredient { get; set; }

        public int IdRecipe { get; set; }

        public string Quantity { get; set; } = null!;
    }
}
