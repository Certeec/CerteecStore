using CerteecStore.Application.Carts;
using CerteecStore.Application.Products;
using Microsoft.AspNetCore.Mvc;

namespace CerteecStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartsController : Controller
    {
        private CartService _cartService;
        private readonly IProductRepository _productRepository;

        public CartsController()
        {
            InMemoryCartRepository cartRepository = new InMemoryCartRepository();
            _productRepository = new InMemoryProductRepository();
            _cartService = new CartService(cartRepository, _productRepository);
        }

        [HttpGet("FindCartById{userId}")]
        public IActionResult FindCartByUserId(Guid userId)
        {
            Cart userCart = _cartService.FindOrCreateCartByUserId(userId);
            return Ok(userCart);
        }

        [HttpGet("CountCartValue{userId}")]
        public IActionResult CountCartValue(Guid userId)
        {
            double value = _cartService.CountCartValue(userId);
            return Ok(value);
        }

        [HttpPost("AddProductToCart{userId}/{productId}/{quantity}")]
        public IActionResult AddProductToCart(Guid userId, int productId, int quantity)
        {
            _cartService.AddProductToCart(userId, productId, quantity);
            return Ok();
        }

        [HttpDelete("RemoveSingleQuantityOfProductInCart{userId}/{productId}")]
        public IActionResult DeleteOneItemOfProductInCart(Guid userId, int productId)
        {
            int quantityLeft = _cartService.TakeProductFromTheCart(userId, productId);
            return Ok(quantityLeft);
        }

        [HttpGet("ShowAllProductsInCart{userId}")]
        public IActionResult ShowAllProductsInCart(Guid userId)
        {
            return Ok(_cartService.ShowAllProductsInCart(userId));
        }
    }

}
