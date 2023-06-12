using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Products
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly string _ConnectionString;
        public DapperProductRepository(IConfiguration configuration)
        {
            _ConnectionString = configuration["ConnectionString"];
        }

        public List<Product> ReadAllProducts()
        {
            using (SqlConnection sqlCon = new SqlConnection(_ConnectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<Product>(
                    "SELECT ProductId, Name, Description, ItemPrice, Quantity FROM Products").ToList();
            }
        }

        public int RemoveProductById(int productId)
        {
            using (SqlConnection sqlCon = new SqlConnection(_ConnectionString))
            {
                sqlCon.Open();
                return sqlCon.Execute(@"DELETE FROM Products WHERE ProductId = @ProductId",
                    new { ProductId = productId });
            }
        }

        public Product? FindProductById(int productId)
        {
            using (SqlConnection sqlCon = new SqlConnection(_ConnectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<Product>(
                    @"SELECT ProductId, Name, Description, ItemPrice, Quantity FROM Products WHERE ProductId = @ProductId",
                    new { ProductId = productId }).FirstOrDefault();
            }
        }

        public int AddProduct(Product productToadd)
        {
            using (SqlConnection sqlCon = new SqlConnection(_ConnectionString))
            {
                sqlCon.Open();
                string query = "INSERT INTO Products (Name, Description, ItemPrice, Quantity) VALUES (@Name, @Description, @ItemPrice, @Quantity)";
                var dp = new DynamicParameters();
                dp.Add("@ProductId", productToadd.ProductId);
                dp.Add("@Name", productToadd.Name);
                dp.Add("@Description", productToadd.Description);
                dp.Add("@ItemPrice", productToadd.ItemPrice);
                dp.Add("@Quantity", productToadd.Quantity);

                return sqlCon.Execute(query, dp);
            }
        }

        public List<Product> ReadProductsByArray(int[] productsIds)
        {
            Dictionary<string, int> parameters = new Dictionary<string, int>();

            for(int i =0; i< productsIds.Length; i++)
            {
                parameters.Add($"@ProductId{i}", productsIds[i]);
            }

            using(SqlConnection sqlCon = new SqlConnection(_ConnectionString))
            {
                sqlCon.Open();
                string query = $"SELECT ProductId, Description, Name, ItemPrice, Quantity FROM Products WHERE ProductId IN ({string.Join(",", parameters.Keys)})";
                var dp = new DynamicParameters();
                foreach(var parameter in parameters)
                {
                    dp.Add(parameter.Key, parameter.Value);
                }
                return sqlCon.Query<Product>(query, dp).ToList();
            }
        }
    }
}
