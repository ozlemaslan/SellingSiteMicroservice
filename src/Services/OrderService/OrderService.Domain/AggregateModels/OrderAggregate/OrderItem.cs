using OrderService.Domain.SeedWork;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem(int productId, string productName, decimal unitPrice, int units, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Units = units;
            PictureUrl = pictureUrl;
        }
        public OrderItem()
        {
            
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Units { get; set; } = 1;
        public string PictureUrl { get; set; }
    }
}
