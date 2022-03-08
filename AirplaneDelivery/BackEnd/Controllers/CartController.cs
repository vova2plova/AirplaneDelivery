using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    public class CartController : Controller
    {
        private DatabaseContext db;

        public CartController(DatabaseContext context)
        {
            db = context;
        }

        [HttpPost("AddSpotToUserCart/{id}")]
        public async Task<ActionResult> AddSpotToCart(Spot spot, int id)
        {
            if (spot.Products != null && spot.Count > 0)
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    var spotInCart = user.Cart.Spots.FirstOrDefault(s => s.Products.Name == spot.Products.Name);
                    var productInStorage = db.Products.FirstOrDefault(p => p.Name == spot.Products.Name);
                    if (productInStorage != null)
                    {
                        if (spotInCart != null)
                        {
                            if ((productInStorage.CountInStorage - (spotInCart.Count + spot.Count)) > 0)
                                spotInCart.Count += spot.Count;
                            else
                                return BadRequest($"Можно добавить только {productInStorage.CountInStorage} продуктов");
                        }
                        else
                        {
                            if ((productInStorage.CountInStorage - spot.Count) > 0)
                                user.Cart.Spots.Add(spot);
                            else
                                return BadRequest($"Можно добавить только {productInStorage.CountInStorage} продуктов");
                        }
                        await db.SaveChangesAsync();
                        return Ok("Продукт добавлен в корзину");
                    }
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
