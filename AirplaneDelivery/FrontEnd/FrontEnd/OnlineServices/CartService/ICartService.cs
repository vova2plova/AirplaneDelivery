using DAL.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.OnlineServices.CartService
{
    public interface ICartService
    {
        [Get("/Cart/GetUserCart/{id}")]
        Task<ApiResponse<List<Spot>>> GetUserCart(int id);

        [Post("/Cart/AddSpotToUserCart/{id}")]
        Task<ApiResponse<Spot>> AddSpotToCart(Spot spot, int id);
        [Put("/Cart/EditCountProducts")]
        Task<Spot> EditCountProducts(int idSpot, int NewCount);
    }
}
