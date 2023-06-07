using CerteecStore.Application.Products;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Carts
{
    public class CartRepository : ICartRepository
    {
        private readonly string _connectionString;

        public CartRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"];
        }

        public ProductsInCart GetProductQuantity(int userId, int productId)
        {
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlCon;
                cmd.CommandText = @"Select ProductId, Quantity FROM UserCarts Where UserId = @UserId And ProductId = @ProductId";
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        ProductsInCart productToReturn = new ProductsInCart()
                        {
                            ProductId = dr.GetInt32("ProductId"),
                            Quantity = dr.GetInt32("Quantity")
                        };
                        return productToReturn;

                    }
                }
                return null;

            }
        }

        public int InsertIntoCart(int userId, int productId, int quantity)
        {
            using(SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlCon;
                cmd.CommandText = @"INSERT INTO UserCarts (UserId, ProductId, Quantity) VALUES (@UserId, @ProductId, @Quantity)";
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);

                return cmd.ExecuteNonQuery();
            }
        }

        public int UpdateQuantityInCart(int userId, int productId, int quantity)
        {
            using(SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlCon;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE UserCarts SET Quantity = @Quantity WHERE UserId = @UserId AND ProductId = @ProductId";
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue(@"ProductId", productId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                return cmd.ExecuteNonQuery();

            }

        }

        public List<ProductsInCart> ShowAllProductsInCart(int userId)
        {
            List<ProductsInCart> cart = new List<ProductsInCart>();
            using(SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT ProductId, Quantity FROM UserCarts WHERE UserId = @UserId;";
                cmd.Connection = sqlCon;
                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ProductsInCart productInCart = new ProductsInCart()
                        {
                            ProductId = dr.GetInt32("ProductId"),
                            Quantity = dr.GetInt32("Quantity")
                        };
                        cart.Add(productInCart);
                    }
                }
                else
                {
                    Console.WriteLine("No data found.");
                }

                return cart;
            }
        }
        
        public int RemoveProductFromCart(int userId, int productId)
        {
            using(SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM UserCarts WHERE UserId = @UserId AND ProductId = @ProductId";
                cmd.Connection = sqlCon;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
