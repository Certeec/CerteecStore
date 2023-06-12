using CerteecStore.Application.Products;
using Dapper;
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
    public class DapperCartRepository : ICartRepository
    {
        private readonly string _ConnectionString;
        public DapperCartRepository(IConfiguration configuration)
        {
            _ConnectionString = configuration["ConnectionString"];
        }

        public List<ProductsInCart> ShowAllProductsInCart(int userId)
        {
            List<ProductsInCart> cart = new List<ProductsInCart>();

            using(SqlConnection con = new SqlConnection(_ConnectionString))
            {
                con.Open();
                cart = con.Query<ProductsInCart>(
                    "SELECT ProductId, Quantity FROM UserCarts WHERE userId = @UserId",
                    new { UserId = userId}).ToList();
                
            }
            return cart;
        }

        public int RemoveProductFromCart(int userId, int productId)
        {
            using (SqlConnection sqlCon = new SqlConnection(_ConnectionString))
            {
                sqlCon.Open();
                var delRows = sqlCon.Execute(
                    @"DELETE FROM UserCarts WHERE UserId = @UserId AND ProductId = @ProductId",
                    new { UserId = userId, ProductId = productId });
                return delRows;
            }

        }

        public ProductsInCart GetProductQuantity(int userId, int productId)
        {

            using (SqlConnection sqlCon = new SqlConnection(_ConnectionString))
            {
                sqlCon.Open();
                var quantity = sqlCon.ExecuteScalar<int>
                    ("SELECT Quantity FROM UserCarts WHERE UserId = @UserId AND ProductId = @ProductId" 
                    , new { UserId = userId, ProductId = productId});

                if(quantity == 0)
                {
                    return (ProductsInCart)null;
                }
                        
                return new ProductsInCart()
                {
                    ProductId = productId,
                    Quantity = quantity
                };
            }
        }
        
        public int InsertIntoCart(int userId, int productId, int quantity)
        {
            using(SqlConnection sqlCon = new SqlConnection(_ConnectionString))
            {
                sqlCon.Open();
                string query = @"INSERT INTO UserCarts (UserId, ProductId, Quantity) VALUES (@UserId, @ProductId, @Quantity)";
                var dp = new DynamicParameters();
                dp.Add("@UserId", userId);
                dp.Add("@ProductId", productId);
                dp.Add("@Quantity", quantity);

                return sqlCon.Execute(query, dp);
            }
        }

        public int UpdateQuantityInCart(int userId, int productId, int quantity)
        {
            using(SqlConnection sqlCon = new SqlConnection(_ConnectionString))
            {
                sqlCon.Open();
                return sqlCon.Execute("UPDATE UserCarts SET Quantity = @Quantity WHERE Userid = @UserId AND ProductId = @ProductId",
                    new { Quantity = quantity, UserId = userId, ProductId = productId });
            }
        }

    }
}
