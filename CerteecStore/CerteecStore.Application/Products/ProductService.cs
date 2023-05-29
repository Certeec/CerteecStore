using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Products
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> ReadAll()
        {
            return _productRepository.ReadAll();
        }

        public int RemoveProductById(int id)
        {
           return _productRepository.RemoveProductById(id);
        }

        public Product FindProductById(int productId)
        {
            return _productRepository.FindProductById(productId);
        }

        public bool AddProduct(Product productToAdd)
        {
            return _productRepository.AddProduct(productToAdd);
        }
    }
}
