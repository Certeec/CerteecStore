using Microsoft.AspNetCore.Mvc;
using CerteecStore.Application.Products;

namespace CerteecStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private IProductRepository _productRepository;

        public ProductsController()
        {
            _productRepository = new InMemoryProductRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("ShowProducts")]
        public IActionResult ShowAllProducts()
        {
            List<Product> productList = _productRepository.ReadAll();

            return Ok(productList);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody]Product product)
        {
            _productRepository.AddProduct(product);
            Console.WriteLine("New Item Added");
            return Ok(product);
        }

        /// tu sie powinno pojawic jeszcze remove product?
        /// pytanie czy powinno sie dac usunac produkt normalnie z listy..
        /// czy moze lepiej zrobic cos w stylu bIsAcitve 0/1;
        
        // To zależy od wymagań biznesowych zawsze. Według mnie produkt nie jest rzeczą "trwałą", która zawsze powinna istnieć.
        // Produkty się usuwa, potem dodaje itd. to nie ma wpływu na działanie programu. Inna sprawa może się mieć z użytkownikiem,
        // którego mógłbyś się zastanowić czy usuwać jak usuwa konto czy lepiej go zdezaktywować, bo może mieć zależności do jakichś transakcji itd.
    }
}
