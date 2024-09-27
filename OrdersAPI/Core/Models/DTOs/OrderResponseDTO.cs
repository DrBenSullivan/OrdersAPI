#pragma warning disable 1591

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
    

        public Order ToOrder() => new ()
        {
            OrderId = OrderId,
            OrderNumber = OrderNumber,
            CustomerName = CustomerName,
            OrderDate = OrderDate,
            TotalPrice = TotalPrice
        };
    } 

    public static class OrderExtensions
    {
        public static OrderResponseDTO ToOrderResponseDTO(this Order order) => new ()
        {
            OrderId = order.OrderId,
            OrderNumber = order.OrderNumber,
            CustomerName = order.CustomerName,
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPrice,
            OrderItems = order.Items.ToOrderItemResponseDTOList()
        };

        public static List<OrderResponseDTO> ToOrderResponseDTOList(this List<Order> orders)
        {
            var responseList = new List<OrderResponseDTO>();
            foreach (var order in orders)
            {
                responseList.Add(order.ToOrderResponseDTO());
            }
            return responseList;
        }
    }
}
