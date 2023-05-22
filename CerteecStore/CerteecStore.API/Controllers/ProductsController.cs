﻿using Microsoft.AspNetCore.Mvc;
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
        
    }
}
