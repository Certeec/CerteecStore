using CerteecStore.Application.Carts;
using CerteecStore.Application.Products;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335; // Nieużywane usingi - do usunięcia

namespace CerteecStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartsController : Controller
    {
        private ICartService _cartService; // zmienne w "serwisach" powinny być private readonly - chyba nie chcesz żeby ktoś Ci podmienił implementację w trakcie

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("FindCartById{userId}")]
        public IActionResult FindCartByUserId(Guid userId)
        {
            Cart userCart = _cartService.FindCartByUserId(userId);
            return userCart != null ? Ok(userCart) : NotFound();
            //Midleware (google it) /// Komentarz - do usunięcia
        }

        [HttpGet("CountCartValue{userId}")] // Powinno być CountCartValue/{userId} - URL jednak lepiej wygląda jako CountCartValue/123
        public IActionResult CountCartValue(Guid userId)
        {
            double value = _cartService.CountCartValue(userId);
            return Ok(value);
        }

        [HttpPost("AddProductToCart{userId}/{productId}/{quantity}")] // Według mnie lepiej by było product id i quantity podawać przez [FromBody] zamiast ze ścieżki. Poczytaj o dobrych praktykach REST
        public IActionResult AddProductToCart(Guid userId, int productId, int quantity)
        {
            var result = _cartService.AddProductToCart(userId, productId, quantity);
            return result == true ? Ok() : NotFound();
        }

        [HttpDelete("RemoveSingleQuantityOfProductInCart{userId}/{productId}")] // slash po nazwie
        public IActionResult DeleteOneItemOfProductInCart(Guid userId, int productId)
        {
            int quantityLeft = _cartService.RemoveOneProductFromTheCart(userId, productId);
            return quantityLeft != -1 ? Ok(quantityLeft) : NotFound(); 
        }

        [HttpGet("ShowAllProductsInCart{userId}")] // zgodnie z REST ścieżka mogłaby być carts/{userId}/products
        public IActionResult ShowAllProductsInCart(Guid userId)
        {
            return Ok(_cartService.ShowAllProductsInCart(userId));
        }
    }

}
