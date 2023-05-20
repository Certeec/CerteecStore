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
using CerteecStore.Application.Database;
using CerteecStore.Application.Products;

namespace CerteecStore.ConsoleApp
{
    public class Program
    {
        static IProductRepository _productRepository;
        static ICartRepository _cartRepository; // odstęp pomiędzy funkcją i właściwością
        public static void Main()
        {
            Initialize();
            Menu();
            Console.WriteLine(InMemoryDatabase.Prodcuts[0].Description);
            Console.WriteLine(InMemoryDatabase.Prodcuts[1].Name);
           bool result = _productRepository.RemoveByProductId(3); // wcięcie
           Console.WriteLine(JsonConvert.SerializeObject(_productRepository.ReadAll())); // wcięcie
            CartService cart = new CartService(_cartRepository, _productRepository);
            Guid user1 = Guid.NewGuid();
            Guid user2 = Guid.NewGuid();
            cart.AddProductToCart(user1, 1, 1);
            cart.AddProductToCart(user2, 2, 3);
            cart.AddProductToCart(user2, 2, 1);
            InMemoryDatabase.Carts.Count();
        }

        private static void Initialize()
        {
            // pusta linia
            if(Config.DbType == "InMemory") // zastanawiam się czy DbType nie lepiej zrobić jako enum, zauważ, że dostępne wartości sa skończone
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
        private static void secondMenu() // nieużywana metoda
        {

        }
    }
}
