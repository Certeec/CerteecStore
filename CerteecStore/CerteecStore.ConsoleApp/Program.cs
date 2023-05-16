using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CerteecStore.Application;
using CerteecStore.Application.Carts;
using Newtonsoft.Json;

namespace CerteecStore.ConsoleApp
{
    public class Program
    {
        static IProductRepository _productRepository;
        static ICartRepository _cartRepository;
        public static void Main()
        {
            Initialize();
            Menu();
            Console.WriteLine(InMemoryDatabase.Prodcuts[0].Description);
            Console.WriteLine(InMemoryDatabase.Prodcuts[1].Name);
           bool result = _productRepository.RemoveByProductId(3);
           Console.WriteLine(JsonConvert.SerializeObject(_productRepository.ReadAll()));
            CartService cart = new CartService(_cartRepository);
            Guid user1 = Guid.NewGuid();
            Guid user2 = Guid.NewGuid();
            cart.AddProductToCart(user1, 1, 1);
            cart.AddProductToCart(user2, 2, 3);
            cart.AddProductToCart(user2, 2, 1);
            InMemoryDatabase.Carts.Count();
        }

        private static void Initialize()
        {
            
            if(Config.DbType == "InMemory")
            {
                _productRepository = new InMemoryProductRepository();
                _cartRepository = new InMemoryCartRepository();
            }
            else if(Config.DbType == "Sql")
            {
                
            }
        }

        private static void Menu()
        {
            Console.WriteLine("type Key to continue: ");
            ConsoleKeyInfo pressedKey = Console.ReadKey();
            switch (pressedKey.Key)
            {
                case ConsoleKey.D0:
                    Console.WriteLine("Provide URL");
                    string url = Console.ReadLine();
                    InMemoryDatabase.ReadProductsFromFile(url); break;
                case ConsoleKey.D1:
                    break;

            }
        }
        private static void secondMenu()
        {

        }
    }
}
