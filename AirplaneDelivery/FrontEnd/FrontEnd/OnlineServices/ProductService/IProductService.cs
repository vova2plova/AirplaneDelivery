using DAL.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.OnlineServices.ProductService
{
    public interface IProductService
    {
        [Get("/Product/GetAllProducts")]
        Task<ApiResponse<List<Product>>> GetAllProducts();
    }
}
