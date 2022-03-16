using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private DatabaseContext db;

        public ProductController(DatabaseContext context)
        {
            db = context;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await db.Products.Include(p => p.CategoryProduct).ToListAsync();
            return products == null ?
                NotFound() :
                Ok(products); 
        }

        [HttpGet("GetProductByCategory/{id}")]
        public Task<List<Product>> GetProductsByCategory(int id)
        {
            return db.Products.Where(p => p.CategoryProduct.Id == id).ToListAsync();
        }
        [HttpGet("GetProductById/{id}")]
        public Task<Product> GetProductById(int id)
        {
            return db.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
