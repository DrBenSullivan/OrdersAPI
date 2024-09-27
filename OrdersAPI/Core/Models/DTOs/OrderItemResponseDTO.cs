#pragma warning disable 1591

namespace OrdersAPI.Core.Models.DTOs
{
	public class OrderItemResponseDTO
	{
		public Guid OrderItemId { get; set; }
		public Guid OrderId { get; set; }
		public string? ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal TotalPrice { get; set; }
	

		public OrderItem ToOrderItem() => new ()
		{
			OrderItemId = OrderItemId,
			OrderId = OrderId,
			ProductName = ProductName,
			Quantity = Quantity,
			UnitPrice = UnitPrice,
			TotalPrice = TotalPrice
		};
	}

	public static class OrderItemExtensions
	{
		public static OrderItemResponseDTO ToOrderItemResponseDTO(this OrderItem orderItem) => new ()
		{
			OrderItemId = orderItem.OrderItemId,
			OrderId = orderItem.OrderId,
			ProductName = orderItem.ProductName,
			Quantity = orderItem.Quantity,
			UnitPrice = orderItem.UnitPrice,
			TotalPrice = orderItem.TotalPrice
		};

		public static List<OrderItemResponseDTO> ToOrderItemResponseDTOList(this List<OrderItem> orderItems)
		{
			var responseList = new List<OrderItemResponseDTO>();
			foreach (var item in orderItems)
			{
				responseList.Add(item.ToOrderItemResponseDTO());
			}
			return responseList;
		}
	}
}
