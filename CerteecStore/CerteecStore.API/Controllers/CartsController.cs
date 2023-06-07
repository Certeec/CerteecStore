using CerteecStore.API.Requests;
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

        [HttpGet("{userId}/CountCartValue")] 
        public IActionResult CountCartValue(int userId)
        {
            decimal value = _cartService.CountCartValue(userId);
            return Ok(value);
        }

        [HttpPost("{userId}/Products")] // Według mnie lepiej by było product id i quantity podawać przez [FromBody] zamiast ze ścieżki. Poczytaj o dobrych praktykach REST
        public IActionResult AddProductToCart(int userId, [FromBody] List<AddProductsToCartDTO> request)
        {
            int productsNotAdded = 0;
            foreach(var product in request)
            {
                var result = _cartService.AddProductToCart(userId, product.ProductId, product.Quantity);

                if(result == 0)
                {
                    productsNotAdded++;
                }
            }

            return productsNotAdded > 0 ? BadRequest(productsNotAdded) : Ok();
        }

        [HttpDelete("{userId}/Products/{productId}")]
        public IActionResult DeleteOneItemOfProductInCart(int userId, int productId)
        {
            int rowsDeleted = _cartService.RemoveOneProductFromTheCart(userId, productId);
            return rowsDeleted > 0 ? Ok() : NotFound(); 
        }

        [HttpGet("{userId}/products")]
        public IActionResult ShowAllProductsInCart(int userId)
        {
            return Ok(_cartService.ShowAllProductsInCart(userId));
        }
    }

}
 