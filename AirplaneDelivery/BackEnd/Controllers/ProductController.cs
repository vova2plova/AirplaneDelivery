using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
        public List<Product> GetAllProducts()
        {
            return db.Products.ToList();
        }
    }
}
