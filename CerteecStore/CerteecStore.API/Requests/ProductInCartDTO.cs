
namespace CerteecStore.Application.Carts // poprawic
{
    public class ProductInCartDTO
    {
        public double ProductId { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice => UnitPrice * Quantity;
    }
}
