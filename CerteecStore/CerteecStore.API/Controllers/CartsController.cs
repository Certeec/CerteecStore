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

        public CartsController()
        {
            _cartRepository = new InMemoryCartRepository();
        }



        [HttpGet("FindCartById{userId}")]
        public IActionResult FindCartByUserId(Guid userId)
        {
            Cart userCart = _cartRepository.FindOrCreateCartByUserId(userId);
            return Ok(userCart);
        }
    }

}
