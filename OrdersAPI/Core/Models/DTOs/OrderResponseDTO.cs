namespace OrdersAPI.Core.Models.DTOs
{
    public class OrderResponseDTO
    {
        public Guid OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public string? CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemResponseDTO> OrderItems { get; set; }
    }
}
