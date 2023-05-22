﻿using Microsoft.AspNetCore.Mvc;
using CerteecStore.Application.Products;
using CerteecStore.Application.Database;

namespace CerteecStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InMemoryDatabaseController : Controller
    // Jeśli chcesz zrobić coś takiego to bardziej by się nadawał ExportDatabaseController, a sam export bym zrobił w jakimś osobnym serwisie.
    // Dzięki temu będziesz mógł w przyszłości eksportować sobie każdą bazę danych do pliku

        ///Tylko to potrzebowalem na chwile.. a nie wiedzialem jak zrobic auto zapisywanie przy zamykaniu programu, watpie 
        ///abysmy potrzebowali zapisywanie do pliku na stale...
    {
        private IProductRepository _productRepository; //nieużywane

        [HttpPost("SaveDatabaseToFile")]
        public IActionResult SaveDatabaseToFile()
        {
            Console.WriteLine("Saved Database");
            // Według mnie nazwa jest niewdzięczna, bo jak robisz eksport to powinieneś eksportować wszystko, a nie tylko produkty.
            bool result = InMemoryDatabase.SaveProductsToFile();
            return Ok(result);
        }

        // Ogólnie to według mnie bez sensu szukać specjalnego mechanizmu, który wyeksportuje przy wyłączaniu programu,
        // lepiej zostawić jak jest, że eksportujesz na żadanie tak jak teraz.

    }
}
