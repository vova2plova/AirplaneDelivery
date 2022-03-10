using DAL.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.OnlineServices.CategoryService
{
    public interface ICategoryService
    {
        [Get("/Category/GetCategories")]
        Task<ApiResponse<List<Category>>> GetCategories();
    }
}
