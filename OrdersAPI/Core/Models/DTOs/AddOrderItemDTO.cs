using System.Runtime.CompilerServices;

namespace OrdersAPI.Core.Models.DTOs
{
	public class AddOrderItemDTO
	{
		public Guid OrderId { get; set; }
		public string? ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
	
		public OrderItem ToOrderItem() => new ()
		{
			OrderId = OrderId,
			ProductName = ProductName,
			Quantity = Quantity,
			UnitPrice = UnitPrice,
			TotalPrice = Quantity * UnitPrice
		};
	}

	public static class AddOrderItemDTOExtensions
	{
		public static List<OrderItem> ToOrderItemsList(this List<AddOrderItemDTO> dtos) {
			var orderItems = new List<OrderItem>();
			foreach (var item in dtos)
			{
				orderItems.Add(item.ToOrderItem());
			}
			return orderItems;
		}
	}
}
