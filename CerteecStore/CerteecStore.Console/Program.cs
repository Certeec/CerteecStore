using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CerteecStore.Application;

namespace CerteecStor
{
    /// <summary>
    ///  i Tutaj mam problem... Jak zmieniam namespace na CerteecStore.Console to wtedy mi nie lapie usingow... cos robie nie tak.. tylko nie wiem co :P wiec musimy o to zachaczyc 
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            Menu();
            Console.WriteLine(InMemoryDatabase.ListOfProducts[0].Description);
            Console.WriteLine(InMemoryDatabase.ListOfProducts[1].Name);
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

            }
        }
    }
}
