using System;

namespace ShoeStoreApp.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDetails { get; set; }
        public DateTime DateOrder { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int PickupPointID { get; set; }
        public string AuthorizedClientFullName { get; set; }
        public int CodeToReceive { get; set; }
        public string OrderStatus { get; set; }
        
    }
}