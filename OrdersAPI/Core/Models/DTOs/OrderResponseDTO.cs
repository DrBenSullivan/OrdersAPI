namespace OrdersAPI.Core.Models.DTOs
{
	public class OrderResponseDTO
    {
        public Guid OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public string? CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public static class OrderResponseDTOExtensions
    {
        public static Order ToOrder(this OrderResponseDTO dto) => new Order
        {
            OrderId = dto.OrderId,
            OrderNumber = dto.OrderNumber,
            CustomerName = dto.CustomerName,
            OrderDate = dto.OrderDate,
            TotalPrice = dto.TotalPrice
        };
    }

    public static class OrderExtensions
    {
        public static OrderResponseDTO ToOrderResponseDTO(this Order order) => new OrderResponseDTO
        {
            OrderId = order.OrderId,
            OrderNumber = order.OrderNumber,
            CustomerName = order.CustomerName,
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPrice
        };
    }
}
