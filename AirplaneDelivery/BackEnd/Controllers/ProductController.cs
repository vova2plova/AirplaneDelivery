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
        public Task<List<Product>> GetAllProducts()
        {
            return db.Products.ToListAsync();
        }

        [HttpGet("GetProductByCategory/{Category}")]
        public Task<List<Product>> GetProductsByCategory(string Category)
        {
            return db.Products.Where(p => p.Category == Category).ToListAsync();
        }
        [HttpGet("GetProductById/{id}")]
        public Task<Product> GetProductById(int id)
        {
            return db.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
