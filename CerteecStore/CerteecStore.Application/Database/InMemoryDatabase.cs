﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CerteecStore.Application.Carts;
using CerteecStore.Application.Products;

namespace CerteecStore.Application.Database
{
    public static class InMemoryDatabase
    {
        public static List<Product> Prodcuts = new List<Product>();

        public static Dictionary<Guid, Cart> Carts = new Dictionary<Guid, Cart>();

        public static void ReadProductsFromFile()
        {
            string url = "C:\\Users\\AiutJeKokot\\Desktop\\Repozytorium\\certProj\\CerteecStore\\test.txt";
            try
            {
                using (StreamReader sr = new StreamReader(url))
                {
                    string srLine;
                    while ((srLine = sr.ReadLine()) != null)
                    {
                        Prodcuts.Add(ReadProductFromString(srLine));
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"the errors is : {e}");
            }
        }

        private static Product ReadProductFromString(string line)
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

        public static bool SaveProductsToFile()
        {
            string url = "C:\\Users\\AiutJeKokot\\Desktop\\Repozytorium\\certProj\\CerteecStore\\test.txt";
            try
            {
                using (StreamWriter wr = new StreamWriter(url, false))
                {
                    foreach(Product currentProduct in Prodcuts)
                    {
                        string productInString = ProductInString(currentProduct);
                        if (productInString != null) wr.WriteLine(productInString);
                    }
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }

        private static string ProductInString(Product productToConvert)
        {
            string stringToWrite = $"{productToConvert.ProductId},{productToConvert.Name},{productToConvert.Description},{productToConvert.ItemPrice},{productToConvert.Quantity}";

            return stringToWrite;
        }

    }
}