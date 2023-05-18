using Microsoft.AspNetCore.Mvc;
using CerteecStore.Application.Products;
using CerteecStore.Application.Database;

namespace CerteecStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InMemoryDatabaseController : Controller
    {
        private IProductRepository _productRepository;

        [HttpPost("SaveDatabaseToFile")]
        public IActionResult SaveDatabaseToFile()
        {
            Console.WriteLine("Saved Database");
           bool result = InMemoryDatabase.SaveProductsToFile();
            return Ok(result);
        }

    }
}
