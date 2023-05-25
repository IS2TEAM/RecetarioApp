using AutoMapper;
using RecetarioApp.Models;
using RecetarioApp.Models.ModelDTO;

namespace RecetarioApp.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping() { 
            CreateMap<IngredientDTO, Ingredient>();
            CreateMap<UserDTO, User>();
            CreateMap<RecipeDTO, Recipe>();
        }
    }
}
