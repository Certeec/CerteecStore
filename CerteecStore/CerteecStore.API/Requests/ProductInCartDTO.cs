
namespace CerteecStore.API.Requests
{
    public class ProductInCartDTO
    {
        public double ProductId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
