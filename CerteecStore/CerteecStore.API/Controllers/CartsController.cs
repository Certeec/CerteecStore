using CerteecStore.Application.Carts;
using Microsoft.AspNetCore.Mvc;

namespace CerteecStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartsController : Controller
    {
        private readonly ICartService _cartService; 

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("Cart/{userId}")]
        public IActionResult FindCartByUserId(Guid userId)
        {
            Cart userCart = _cartService.FindCartByUserId(userId);
            return userCart != null ? Ok(userCart) : NotFound();
        }

        [HttpGet("CountCartValue/{userId}")] 
        public IActionResult CountCartValue(Guid userId)
        {
            decimal value = _cartService.CountCartValue(userId);
            return Ok(value);
        }

        [HttpPost("Product/{userId}/{productId}/{quantity}")] // Według mnie lepiej by było product id i quantity podawać przez [FromBody] zamiast ze ścieżki. Poczytaj o dobrych praktykach REST
        public IActionResult AddProductToCart(Guid userId, int productId, int quantity)
        {
            var result = _cartService.AddProductToCart(userId, productId, quantity);
            return result == true ? Ok() : NotFound();
        }

        [HttpDelete("RemoveSingleQuantityOfProductInCart/{userId}/{productId}")]
        public IActionResult DeleteOneItemOfProductInCart(Guid userId, int productId)
        {
            int quantityLeft = _cartService.RemoveOneProductFromTheCart(userId, productId);
            return quantityLeft != -1 ? Ok(quantityLeft) : NotFound(); 
        }

        [HttpGet("carts/{userId}/products")]
        public IActionResult ShowAllProductsInCart(Guid userId)
        {
            return Ok(_cartService.ShowAllProductsInCart(userId));
        }
    }

}
 