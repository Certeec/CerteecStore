using CerteecStore.Application.Carts;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {  
            _connectionString = configuration["ConnectionString"];
        }
           
        public Product? FindProductById(int productId)
        {
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                string query = "SELECT ProductId, Name, Description, ItemPrice, Quantity FROM Products WHERE ProductId = @ProductId;";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlCon;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@ProductId", productId);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while(dr.Read())
                    {
                        Product productToReturn = new Product()
                        {
                            ProductId = dr.GetInt32("ProductId"),
                            Name = dr.GetString("Name"),
                            Description = dr.GetString("Description"),
                            ItemPrice = dr.GetDecimal("ItemPrice"),
                            Quantity = dr.GetInt32("Quantity")
                        };
                        return productToReturn;
                    }
                }
                return null;
            }
        }

        public List<Product> ReadAllProducts()
        {
            List<Product> products = new List<Product>();
            using(SqlConnection sqlCon = new SqlConnection())
            {
                sqlCon.Open();
                string query = "SELECT ProductId, Name, Description, ItemPrice, Quantity FROM Products;";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Connection = sqlCon;
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        Product productToAdd = new Product()
                        {
                            ProductId = dr.GetInt32("ProductId"),
                            Name = dr.GetString("Name"),
                            Description = dr.GetString("Description"),
                            ItemPrice = dr.GetDecimal("ItemPrice"),
                            Quantity = dr.GetInt32("Quantity")
                        };
                        products.Add(productToAdd);
                    }
                }
                return products;
            }
        }

        public int AddProduct(Product productToAdd)
        {
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Products (Name, Description, ItemPrice, Quantity) OUTPUT INSERTED.ProductId VALUES( @Name, @Description, @ItemPrice, @Quantity)";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
   
                cmd.Parameters.AddWithValue("@Name", productToAdd.Name);
                cmd.Parameters.AddWithValue("@Description", productToAdd.Description);
                cmd.Parameters.AddWithValue("@ItemPrice", productToAdd.ItemPrice);
                cmd.Parameters.AddWithValue("@Quantity", productToAdd.Quantity);

                sqlCon.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public int RemoveProductById(int productId)
        {
            int result = 0;
            string query = "DELETE FROM Products WHERE ProductId = @productId";
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@productId", productId);
                result = cmd.ExecuteNonQuery();
            }
            return result;

        }

        public List<Product> ReadProductsByArray(int[] productsIds)
        {
            Dictionary<string, int> parameters = new Dictionary<string, int>();
            
            for(int i =0; i < productsIds.Length; i++)
            {
                parameters.Add($"@ProductId{i}", productsIds[i]);
            }

             List<Product> products = new List<Product>();
             using(SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT ProductId, Description, Name, ItemPrice, Quantity FROM Products WHERE ProductId IN ({string.Join(",", parameters.Keys)})";
                cmd.Connection = sqlCon;
                foreach(var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
              
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while(dr.Read())
                    {
                        Product current = new Product()
                        {
                            ProductId = dr.GetInt32("ProductId"),
                            Description = dr.GetString("Description"),
                            Name = dr.GetString("Name"),
                            ItemPrice = dr.GetDecimal("ItemPrice"),
                            Quantity = dr.GetInt32("Quantity")
                        };
                        products.Add(current);
                    }

                }
                return products;
            }
        }
    }
}
