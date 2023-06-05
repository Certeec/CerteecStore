using CerteecStore.Application.Carts;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository()
        {
            _connectionString = @"Data Source=(local)\SQlEXPRESS; Initial Catalog=Shop; Integrated Security = True; Trusted_Connection=True;TrustServerCertificate=True;";
        }
           

        public Product FindProductById(int productId)
        {
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * FROM Products WHERE ProductId = @ProductId;";
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

        //public List<Product> ReadAllProducts()
        //{
        //    using (SqlConnection sqlCon = new SqlConnection(_connectionString))
        //    {

        //        sqlCon.Open();
        //        SqlDataAdapter sqlData = new SqlDataAdapter("Select * From Products", sqlCon);
        //        DataSet ds = new DataSet();
        //        sqlData.Fill(ds);
        //        List<Product> productList = new List<Product>();

        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            Product productToadd = new Product()
        //            {
        //                ProductId = int.Parse(ds.Tables[0].Rows[i].ItemArray[0].ToString()),
        //                Name = ds.Tables[0].Rows[i].ItemArray[1].ToString(),
        //                Description = ds.Tables[0].Rows[i].ItemArray[2].ToString(),
        //                ItemPrice = decimal.Parse(ds.Tables[0].Rows[i].ItemArray[3].ToString()),
        //                Quantity = int.Parse(ds.Tables[0].Rows[i].ItemArray[4].ToString())
        //            };

        //            productList.Add(productToadd);
        //        }

        //        return productList;
        //    }
        //}

        public List<Product> ReadAllProducts()
        {
            List<Product> products = new List<Product>();
            using(SqlConnection sqlCon = new SqlConnection())
            {
                sqlCon.Open();
                string query = "SELECT * FROM Products;";
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

        public List<Product> ReadProductsByArray(int[] productIds)
        {
             List<Product> products = new List<Product>();
            string any = "1,2,3";
             using(SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Products WHERE ProductId IN (ProductId)";
                cmd.Connection = sqlCon;
                cmd.Parameters.AddWithValue("ProductId", any);
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
