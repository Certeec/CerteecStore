using Microsoft.AspNetCore.Mvc;
using CerteecStore.Application.Products;
using CerteecStore.API.Requests;

namespace CerteecStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productService)
        {
            _productRepository = productService;
        }

        [HttpGet("products")]
        public IActionResult ShowAllProducts()
        {
            List<Product> productList = _productRepository.ReadAllProducts();

            return Ok(productList);
        }

        [HttpGet("products/{productId}", Name = "FindProductById")]
        public IActionResult FindProductById(int productId)
        {
            var returnedProduct = _productRepository.FindProductById(productId);

            return returnedProduct != null ? Ok(returnedProduct) : NotFound();
        }

        [HttpPost("products")]
        public IActionResult AddProduct([FromBody] CreateProductDTO product)
        {
            try
            {
                Product productToAdd = new Product()
                {
                    Description = product.Description,
                    ItemPrice = product.ItemPrice,
                    Name = product.Name,
                    Quantity = product.Quantity
                };
                int productIdReturned = _productRepository.AddProduct(productToAdd);

                return CreatedAtAction("FindProductById", new { productId = productIdReturned }, null);
            }
            catch(Exception e)
            {
                return BadRequest();
            }

        }

        [HttpDelete("product/{productId}")]
        public IActionResult RemoveProduct(int productId)
        {
            var result = _productRepository.RemoveProductById(productId);

            return result > 0 ? Ok(result) : NotFound();
        }

       
    }
}