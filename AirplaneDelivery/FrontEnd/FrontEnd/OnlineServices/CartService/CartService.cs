using DAL.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.OnlineServices.CartService
{
    class CartService : BaseDataService<ICartService>, ICartService
    {
        public async Task<ApiResponse<User>> AddMissingProductsFromRecipe(int idUser, int idRecipe)
        {
            return await InstanceInterface.AddMissingProductsFromRecipe(idUser, idRecipe);
        }

        public async Task<ApiResponse<User>> AddOrderToHistory(int idUser)
        {
            return await InstanceInterface.AddOrderToHistory(idUser);
        }

        public async Task<ApiResponse<User>> AddProductsFromRecipe(int idUser, int idRecipe)
        {
            return await InstanceInterface.AddProductsFromRecipe(idUser, idRecipe);
        }

        public async Task<ApiResponse<Spot>> AddSpotToCart(Spot spot, int id)
        {
            return await InstanceInterface.AddSpotToCart(spot, id);
        }

        public async Task<Spot> EditCountProducts(int idSpot, int NewCount)
        {
            return await InstanceInterface.EditCountProducts(idSpot, NewCount);
        }

        public async Task<ApiResponse<List<Cart>>> GetHistoryOrder(int id)
        {
            return await InstanceInterface.GetHistoryOrder(id);
        }

        public async Task<ApiResponse<List<Spot>>> GetUserCart(int id)
        {
            return await InstanceInterface.GetUserCart(id);
        }
    }
}
