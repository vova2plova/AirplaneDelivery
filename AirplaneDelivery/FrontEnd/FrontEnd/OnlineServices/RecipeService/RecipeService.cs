using DAL.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.OnlineServices.RecipeService
{
    class RecipeService : BaseDataService<IRecipeService>, IRecipeService
    {
        public async Task<ApiResponse<List<Product>>> GetAllProductsFromRecipe(int id)
        {
            return await InstanceInterface.GetAllProductsFromRecipe(id);
        }

        public async Task<ApiResponse<List<Recipe>>> GetRecipesByIdProducts(int id)
        {
            return await InstanceInterface.GetRecipesByIdProducts(id);
        }
    }
}
