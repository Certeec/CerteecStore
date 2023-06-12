using CerteecStore.API.Requests;
using CerteecStore.Application.Carts;
using Microsoft.AspNetCore.Mvc;
using CerteecStore.API.Requests;

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

        [HttpGet("{userId}/products")]
        public IActionResult ShowAllProductsInCart(int userId)
        {
            var result = _cartService.ShowAllProductsInCart(userId);
            List<ProductInCartDTO> productsInCart = result.Select(n => new ProductInCartDTO
            {
                ProductId = n.Key.ProductId,
                Name = n.Key.Name,
                UnitPrice = n.Key.ItemPrice,
                Quantity = n.Value
            }).ToList();

            return Ok(productsInCart);
        }

        [HttpPost("{userId}/Products")]
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
        public IActionResult DeleteProduct(int userId, int productId)
        {
            int rowsDeleted = _cartService.RemoveProductFromTheCart(userId, productId);
            return rowsDeleted > 0 ? Ok() : NotFound(); 
        }

        [HttpPut("{userId}/Products/{productId}/Increment")]
        public IActionResult IncrementProductToCart(int userId, int productId)
        {
            _cartService.UpdateProductQuantityInCart(userId, productId, x => x + 1);

            return Ok();
        }

        [HttpPut("{userId}/Products/{productId}/Decrement")]
        public IActionResult DecrementProductFromCart(int userId, int productId)
        {
            _cartService.UpdateProductQuantityInCart(userId, productId, x => x - 1);

            return Ok();
        }

        [HttpPut("{userId}/Products/{productId}")]
        public IActionResult DecrementProductFromCart(int userId, int productId, [FromBody] int quantity)
        {
            _cartService.UpdateProductQuantityInCart(userId, productId, x => quantity);

            return Ok();
        }
    }

}
 