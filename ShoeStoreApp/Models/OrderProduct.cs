namespace ShoeStoreApp.Models
{
    public class OrderProduct
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int TovarID { get; set; }
        public int Quantity { get; set; }
    }
}