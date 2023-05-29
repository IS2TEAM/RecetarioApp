namespace RecetarioApp.Models.ModelDTO
{
    public class IngredientsUserDTO
    {
        public int IdIngredient { get; set; }

        public string NameIngredient { get; set; } = null!;

        public string NameShop { get; set; } = null!;

        public int PriceShop { get; set; }
    }
}
