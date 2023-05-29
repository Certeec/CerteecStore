using CerteecStore.Application.Carts;
using CerteecStore.Application.Products;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace CerteecStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartsController : Controller
    {
        private ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("FindCartById{userId}")]
        public IActionResult FindCartByUserId(Guid userId)
        {
            Cart userCart = _cartService.FindCartByUserId(userId);
            return userCart != null ? Ok(userCart) : NotFound();
            //Midleware (google it) 
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
            var result = _cartService.AddProductToCart(userId, productId, quantity);
            return result == true ? Ok() : NotFound();
        }

        [HttpDelete("RemoveSingleQuantityOfProductInCart{userId}/{productId}")]
        public IActionResult DeleteOneItemOfProductInCart(Guid userId, int productId)
        {
            int quantityLeft = _cartService.RemoveOneProductFromTheCart(userId, productId);
            return quantityLeft != -1 ? Ok(quantityLeft) : NotFound(); 
        }

        [HttpGet("ShowAllProductsInCart{userId}")]
        public IActionResult ShowAllProductsInCart(Guid userId)
        {
            return Ok(_cartService.ShowAllProductsInCart(userId));
        }
    }

}
