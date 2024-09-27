#pragma warning disable 1591

namespace OrdersAPI.Core.Models.DTOs
{
	public class UpdateOrderItemDTO
	{
		public Guid OrderId { get; set; }
		public Guid OrderItemId { get; set; }
		public string? ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		
		public OrderItem ToOrderItem() => new ()
		{
			OrderId = OrderId,
			OrderItemId = OrderItemId,
			ProductName = ProductName,
			Quantity = Quantity,
			UnitPrice = UnitPrice,
			TotalPrice = Quantity * UnitPrice
		};
	}
}
