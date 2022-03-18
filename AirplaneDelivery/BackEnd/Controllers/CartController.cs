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
    public class CartController : Controller
    {
        private DatabaseContext db;

        public CartController(DatabaseContext context)
        {
            db = context;
        }

        [HttpPost("AddMissingProductsToCartFromRecipe/{idUser}/{idRecipe}")]
        public async Task<ActionResult<User>> AddMissingProductsFromRecipe(int idUser, int idRecipe)
        {
            var recipe = db.Recipes.Include(p => p.Products).FirstOrDefault(x => x.Id == idRecipe);
            if (recipe != null)
            {
                var users = await db.Users.Include(u => u.Cart).ToListAsync();
                var user = users.FirstOrDefault(u => u.Id == idUser);
                user.Cart.Spots = db.Spots.Where(s => s.CartId == user.Cart.Id).ToList();
                if (user != null)
                {
                    if (user.Cart.Spots == null)
                    {
                        user.Cart.Status = "Shoping";
                        user.Cart.Spots = new List<Spot>();
                    }
                    int i = 0;
                    while (i < recipe.Products.Count)
                    {
                        var spots = db.Spots.FirstOrDefault(s =>
                            s.Products.Name == recipe.Products[i].Name
                            && s.CartId == user.Cart.Id); ;
                        if (spots == null)
                        {
                            var spot = new Spot()
                            {
                                Products = recipe.Products[i],
                                Count = 1
                            };
                            user.Cart.Spots.Add(spot);
                        }
                        i++;
                    }
                    await db.SaveChangesAsync();
                    return Ok("Продукт добавлен в корзину");
                }
            }
            return BadRequest();
        }

        [HttpPost("AddProductsToCartFromRecipe/{idUser}/{idRecipe}")]
        public async Task<ActionResult<User>> AddProductsFromRecipe(int idUser, int idRecipe)
        {
            var recipe = db.Recipes.Include(p => p.Products).FirstOrDefault(x => x.Id == idRecipe);
            if (recipe != null)
            {
                var users = await db.Users.Include(u => u.Cart).ToListAsync();
                var user = users.FirstOrDefault(u => u.Id == idUser);
                user.Cart.Spots = db.Spots.Where(s => s.CartId == user.Cart.Id).ToList();
                if (user != null)
                {
                    if (user.Cart.Spots == null)
                    {
                        user.Cart.Status = "Shoping";
                        user.Cart.Spots = new List<Spot>();
                    }
                    int i = 0;
                    while (i < recipe.Products.Count)
                    {
                        var spots = db.Spots.FirstOrDefault(s => 
                            s.Products.Name == recipe.Products[i].Name 
                            && s.CartId == user.Cart.Id);
                        if (spots != null)
                            spots.Count++;
                        else
                        {
                            var spot = new Spot()
                            {
                                Products = recipe.Products[i],
                                Count = 1
                            };
                            user.Cart.Spots.Add(spot);
                        }
                        i++;
                    }
                    await db.SaveChangesAsync();
                    return Ok("Продукт добавлен в корзину");
                }
            }
            return BadRequest();
        }

        [HttpGet("GetHistoryOrders")]
        public async Task<ActionResult<List<Cart>>> GetHistoryOrder(int id)
        {
            var user = await db.Users.Include(c => c.HistoryOfOrders).ThenInclude(p => p.Spots)
                .ThenInclude(p => p.Products).FirstOrDefaultAsync(u => u.Id == id);
            return Ok(user.HistoryOfOrders);
        }

        [HttpPost("AddOrderToHistory/{idUser}")]
        public async Task<ActionResult<User>> AddOrderToHistory(int idUser)
        {
            var user = await db.Users.Include(c => c.HistoryOfOrders).Include(u => u.Cart).FirstOrDefaultAsync(u => u.Id == idUser);
            if (user != null)
            {
                var cart = db.Carts.FirstOrDefault(c => c.Id == user.Cart.Id);
                var spots = db.Spots.Include(s => s.Products).Where(s => s.CartId == user.Cart.Id).ToList();
                if (spots != null)
                {
                    cart.Spots = spots;
                    cart.Status = "В доставке";
                }
                else
                    return BadRequest();
                user.HistoryOfOrders.Add(cart);
                user.Cart = new Cart()
                {
                    Status = "Покупка"
                };
                await db.SaveChangesAsync();
                return Ok("Успешно");
            }
            return BadRequest("Ошибка");
        }

        [HttpPut("EditCountProducts")]
        public async Task<Spot> EditCountProducts(int idSpot, int NewCount)
        {
            var spot = db.Spots.FirstOrDefault(x => x.Id == idSpot);
            if (NewCount != 0)
                spot.Count = NewCount;
            else
                db.Spots.Remove(spot);
            await db.SaveChangesAsync();
            return spot;
        }

        [HttpGet("GetUserCart/{id}")]
        public async Task<ActionResult<List<Spot>>> GetUserSpots(int id)
        {
            var users = await db.Users.Include(u => u.Cart).ToListAsync();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                var Spots = db.Spots.Where(s => s.CartId == user.Cart.Id);
                await Spots.Include(s => s.Products).ToListAsync();
                return Ok(Spots);
            }
            return BadRequest();
        }


        [HttpPost("AddSpotToUserCart/{id}")]
        public async Task<ActionResult<Spot>> AddSpotToCart(Spot spot, int id)
        {
            if (spot.Products != null)
            {
                var users = await db.Users.Include(u => u.Cart).ToListAsync();
                var user = users.FirstOrDefault(u => u.Id == id);
                user.Cart.Spots = db.Spots.Where(s => s.CartId == user.Cart.Id).ToList();
                if (user != null)
                {
                    if (user.Cart.Spots == null)
                    {
                        user.Cart.Status = "Shoping";
                        user.Cart.Spots = new List<Spot>();
                    }
                    var spots = db.Spots.FirstOrDefault(s => s.Products.Name == spot.Products.Name && s.CartId == user.Cart.Id);
                    if (spots != null)
                        spots.Count += spot.Count;
                    else
                        user.Cart.Spots.Add(spot);
                    await db.SaveChangesAsync();
                    return Ok("Продукт добавлен в корзину");
                }
            }
            return BadRequest();
        }

        [HttpDelete("DeleteProductFromCart/{UserId}/{SpotId}")]
        public async Task<ActionResult> Delete(int UserId,int SpotId)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == UserId);
            if (user != null)
            {
                var cart = user.Cart;
                var spot = cart.Spots.FirstOrDefault(s => s.Id == SpotId);
                if (spot != null)
                {
                    cart.Spots.Remove(spot);
                    await db.SaveChangesAsync();
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpDelete("ClearCart/{UserId}")]
        public async Task<ActionResult> ClearCart(int UserId)
        {
            /*
              db.users.include(u => u.cart).ThenInclude(c=>c.spots)FirstOrdefault
             */
            var user = db.Users.Include(u => u.Cart).ThenInclude(c=>c.Spots).FirstOrDefault(u => u.Id == UserId);
            if (user != null)
            {  
                var cart = user.Cart;
                cart.Spots.Clear();
                await db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
