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
	}

	public static class OrderItemResponseDTOExtensions
	{
		public static OrderItem ToOrderItem(this OrderItemResponseDTO dto) => new ()
		{
			OrderItemId = dto.OrderItemId,
			OrderId = dto.OrderId,
			ProductName = dto.ProductName,
			Quantity = dto.Quantity,
			UnitPrice = dto.UnitPrice,
			TotalPrice = dto.TotalPrice
		};
	}

	public static class OrderItemExtensions
	{
		public static OrderItemResponseDTO ToOrderItemResponse(this OrderItem orderItem) => new ()
		{
			OrderItemId = orderItem.OrderItemId,
			OrderId = orderItem.OrderId,
			ProductName = orderItem.ProductName,
			Quantity = orderItem.Quantity,
			UnitPrice = orderItem.UnitPrice,
			TotalPrice = orderItem.TotalPrice
		};

		public static List<OrderItemResponseDTO> ToOrderItemResponseList(this List<OrderItem> orderItems)
		{
			var responseList = new List<OrderItemResponseDTO>();
			foreach (var item in orderItems)
			{
				responseList.Add(item.ToOrderItemResponse());
			}
			return responseList;
		}
	}
}
