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
    }
}
