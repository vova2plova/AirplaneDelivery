using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : Controller
    {
        private DatabaseContext db;

        public RecipeController(DatabaseContext context)
        {
            db = context;
        }
        [HttpGet("GetAllProductsFromRecipe/{id}")]
        public async Task<ActionResult<List<Product>>> GetAllProductsFromRecipe(int id)
        {
            var recipe = await db.Recipes.Include(p => p.Products).FirstOrDefaultAsync(r => r.Id == id);
            List<Product> products = new List<Product>();
            foreach (var item in recipe.Products)
            {
                var product = db.Products.FirstOrDefault(p => p.Id == item.Id);
                if (product != null)
                {
                    product.Recipe = null;
                    products.Add(product);       
                }
            }
            if (products.Count > 0)
                return Ok(products);
            return NotFound();
        }


        [HttpGet("GetRecipesByIdProducts/{id}")]
        public async Task<ActionResult<List<Recipe>>> GetRecipesByIdProducts(int id)
        {
            var allRecipes = await db.Recipes.Include(p => p.Products).ToListAsync();
            List<Recipe> recipes = new List<Recipe>();
            foreach (var recipe in allRecipes)
            {
                if (recipe.Products.FirstOrDefault(p => p.Id == id) != null)
                {
                    recipe.Products = null;
                    recipes.Add(recipe);
                }
            }
            if (recipes.Count > 0)
                return Ok(recipes);
            return NotFound();
        }
    }
}
