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
        public IActionResult triesPost([FromBody]Product product) //Bardziej bym to nazwał AddProduct, nie musisz pisać "Post", bo już jest na górze, więc wiadomo, że to post ; )
        {
            _productRepository.AddProduct(product);
            Console.WriteLine("New Item Added");
            return Ok(product);
        }
        
    }
}
