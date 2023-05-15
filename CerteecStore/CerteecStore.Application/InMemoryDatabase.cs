using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CerteecStore.Application
{
    public static class InMemoryDatabase
    {
        public static List<Product> ListOfProducts = new List<Product>();
        public static void ReadProductsFromFile(string url)
        {
            try
            {
                using (StreamReader sr = new StreamReader(url))
                {
                    string srLine;
                    while ((srLine = sr.ReadLine())!= null)
                    {
                        ListOfProducts.Add(ReadProductFromFile(srLine));
                    }

                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"the errors is : {e}");
            }
        }
        public static Product ReadProductFromFile(string line)
        {
            string[] parts = line.Split(',');
            return new Product
            {
                ProductId = int.Parse(parts[0]),
                Name = parts[1],
                Description = parts[2],
                ItemPrice = double.Parse(parts[3]),
                Quantity = int.Parse(parts[4])
            };

        }
   
    }
}
