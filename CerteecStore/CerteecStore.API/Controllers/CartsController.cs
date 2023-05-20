using CerteecStore.Application.Carts;
using CerteecStore.Application.Products;
using Microsoft.AspNetCore.Mvc;

namespace CerteecStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartsController : Controller
    {
        private ICartRepository _cartRepository;
        private IProductRepository _productRepository;

        public CartsController()
        {
            _cartRepository = new InMemoryCartRepository();
            _productRepository = new InMemoryProductRepository();
        }



        [HttpGet("FindCartById{userId}")]
        public IActionResult FindCartByUserId(Guid userId)
        {
            Cart userCart = _cartRepository.FindOrCreateCartByUserId(userId);
            return Ok(userCart);
        }

        [HttpGet("CountCartValue{userId}")]
        public IActionResult CountCartValue(Guid userId)
        {
            double value = _cartRepository.CountCartValue(userId);
            return Ok(value);
        }

        [HttpPost("AddProductToCart{userId}/{productId}/{quantity}")]
        public IActionResult AddProductToCart(Guid userId, int productId, int quantity)
        {
            Product productToAdd = _productRepository.FindProductById(productId);
            _cartRepository.AddProductToCart(userId, productToAdd, quantity);
            return Ok();
        }

    }

}
