using Microsoft.AspNetCore.Mvc;
using CerteecStore.Application.Products;

namespace CerteecStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("ShowProducts")]
        public IActionResult ShowAllProducts()
        {
            List<Product> productList = _productService.ReadAll();

            return Ok(productList);
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] Product product)
        {
            _productService.AddProduct(product);

            return Ok(product);
        }

        [HttpDelete("DeleteProduct{productId}")]
        public IActionResult RemoveProduct(int productId)
        {
            var result = _productService.RemoveProductById(productId);

            return result > 0 ? Ok(result) : NotFound();
        }

        [HttpGet("FindProductById{productId}")]
        public IActionResult FindProductById(int productId)
        {
            var returnedProduct = _productService.FindProductById(productId);

            return returnedProduct != null ? Ok(returnedProduct) : NotFound();
        }
    }
}