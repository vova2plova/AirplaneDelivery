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
        public async Task<ApiResponse<Spot>> AddSpotToCart(Spot spot, int id)
        {
            return await InstanceInterface.AddSpotToCart(spot, id);
        }

        public async Task<Spot> EditCountProducts(int idSpot, int NewCount)
        {
            return await InstanceInterface.EditCountProducts(idSpot, NewCount);
        }

        public async Task<ApiResponse<List<Spot>>> GetUserCart(int id)
        {
            return await InstanceInterface.GetUserCart(id);
        }
    }
}
