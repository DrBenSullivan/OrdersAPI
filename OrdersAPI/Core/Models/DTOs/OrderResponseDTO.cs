namespace OrdersAPI.Core.Models.DTOs
{
	public class OrderResponseDTO
    {
        public Guid OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public string? CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemResponseDTO> OrderItems { get; set; } = [];
    }

    public static class OrderResponseDTOExtensions
    {
        public static Order ToOrder(this OrderResponseDTO dto) => new ()
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
        public static OrderResponseDTO ToOrderResponse(this Order order) => new ()
        {
            OrderId = order.OrderId,
            OrderNumber = order.OrderNumber,
            CustomerName = order.CustomerName,
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPrice,
            OrderItems = order.Items.ToOrderItemResponseList()
        };

        public static List<OrderResponseDTO> ToOrderResponseDTOList(this List<Order> orders)
        {
            var responseList = new List<OrderResponseDTO>();
            foreach (var order in orders)
            {
                responseList.Add(order.ToOrderResponse());
            }
            return responseList;
        }
    }
}
