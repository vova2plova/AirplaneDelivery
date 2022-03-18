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
        [Post("/Cart/AddProductsToCartFromRecipe/{idUser}/{idRecipe}")]
        Task<ApiResponse<User>> AddProductsFromRecipe(int idUser, int idRecipe);
        [Post("/Cart/AddMissingProductsToCartFromRecipe/{idUser}/{idRecipe}")]
        Task<ApiResponse<User>> AddMissingProductsFromRecipe(int idUser, int idRecipe);
        [Post("/Cart/AddOrderToHistory/{idUser}")]
        Task<ApiResponse<User>> AddOrderToHistory(int idUser);
        [Get("/Cart/GetHistoryOrders")]
        Task<ApiResponse<List<Cart>>> GetHistoryOrder(int id);

        [Delete("/Cart/ClearCart/{UserId}")]
        Task<ApiResponse<User>> ClearCart(int UserId);
    }
}
