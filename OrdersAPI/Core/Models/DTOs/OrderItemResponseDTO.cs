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
		public static OrderItem ToOrderItem(this OrderItemResponseDTO dto) => new OrderItem
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
		public static OrderItemResponseDTO ToOrderItemResponseDTO(this OrderItem orderItem) => new OrderItemResponseDTO 
		{
			OrderItemId = orderItem.OrderItemId,
			OrderId = orderItem.OrderId,
			ProductName = orderItem.ProductName,
			Quantity = orderItem.Quantity,
			UnitPrice = orderItem.UnitPrice,
			TotalPrice = orderItem.TotalPrice
		};
	}
}
