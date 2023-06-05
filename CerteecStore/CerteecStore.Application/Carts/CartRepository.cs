using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Carts
{
    public class CartRepository
    {
        private readonly string _connectionString;

        public CartRepository()
        {
            _connectionString = @"Data Source=(local)\SQlEXPRESS; Initial Catalog=Shop; Integrated Security = True; Trusted_Connection=True;TrustServerCertificate=True;";
        }

        

        public void AddItemToCart(int userId, int productId, int quantity)
        {
            

            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"
                INSERT INTO [UserCarts]
                ([UserId],
                [ProductId],
                [Quantity])
                VALUES (@UserId,
                @ProductId,
                @Quantity)";
                cmd.Connection = sqlCon;

                cmd.Parameters.AddWithValue("UserId", userId);
                cmd.Parameters.AddWithValue("productId", productId);
                cmd.Parameters.AddWithValue("Quantity", quantity);

                cmd.ExecuteNonQuery();

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
                cmd.CommandText = @"SELECT * FROM UserCarts WHERE UserId = @UserId;";
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
    }
}
