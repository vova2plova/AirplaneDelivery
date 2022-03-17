using DAL.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.OnlineServices.RecipeService
{
    public interface IRecipeService
    {
        [Get("/Recipe/GetRecipesByIdProducts/{id}")]
        Task<ApiResponse<List<Recipe>>> GetRecipesByIdProducts(int id);

        [Get("/Recipe/GetAllProductsFromRecipe/{id}")]
        Task<ApiResponse<List<Product>>> GetAllProductsFromRecipe(int id);
    }
}
