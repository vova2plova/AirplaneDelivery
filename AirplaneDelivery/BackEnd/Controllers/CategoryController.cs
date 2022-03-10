using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private DatabaseContext db;

        public CategoryController(DatabaseContext context)
        {
            db = context;
        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            var Categories = db.Categories.ToListAsync();
            if (Categories != null)
                return Ok(await Categories);
            return NotFound();
        }
    }
}
