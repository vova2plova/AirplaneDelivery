using DAL.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.OnlineServices.CategoryService
{
    class CategoryService : BaseDataService<ICategoryService>, ICategoryService
    {
        public async Task<ApiResponse<List<Category>>> GetCategories()
        {
            return await InstanceInterface.GetCategories();
        }
    }
}
