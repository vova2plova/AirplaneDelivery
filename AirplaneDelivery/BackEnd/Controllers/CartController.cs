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
            var user = db.Users.FirstOrDefault(u => u.Id == UserId);
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
