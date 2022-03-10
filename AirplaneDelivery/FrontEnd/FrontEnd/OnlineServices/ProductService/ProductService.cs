using DAL.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.OnlineServices.ProductService
{
    class ProductService : BaseDataService<IProductService>, IProductService
    {
        public async Task<ApiResponse<List<Product>>> GetAllProducts()
        {
            return await InstanceInterface.GetAllProducts();
        }
    }
}
