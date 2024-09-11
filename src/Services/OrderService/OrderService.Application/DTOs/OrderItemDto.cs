namespace OrderService.Application.DTOs
{
    public class OrderItemDto
    {
        public int ProductId { get; init; }
        public string ProductName { get; init; }
        public decimal UnitPrice { get; init; }
        public int Units { get; set; }
        public string PictureUrl { get; init; }
    }
}
