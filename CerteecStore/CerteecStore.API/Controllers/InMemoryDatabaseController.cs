using Microsoft.AspNetCore.Mvc;
using CerteecStore.Application.Products;
using CerteecStore.Application.Database;

namespace CerteecStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InMemoryDatabaseController : Controller
    // Jeśli chcesz zrobić coś takiego to bardziej by się nadawał ExportDatabaseController, a sam export bym zrobił w jakimś osobnym serwisie.
    // Dzięki temu będziesz mógł w przyszłości eksportować sobie każdą bazę danych do pliku
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
