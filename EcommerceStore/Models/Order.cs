namespace EcommerceStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int BagId { get; set; }

        public int UserId { get; set; }  
        public int Quantity { get; set; }
        public string OrderStatus { get; set; }
        public string OrderDate { get; set; }
    }
}
