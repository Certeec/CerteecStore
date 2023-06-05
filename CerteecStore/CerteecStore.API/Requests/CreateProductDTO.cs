namespace CerteecStore.API.Requests
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double ItemPrice { get; set; }
        public int Quantity { get; set; }
    }
}
