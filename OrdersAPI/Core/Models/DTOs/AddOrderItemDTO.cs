using System.Runtime.CompilerServices;

namespace OrdersAPI.Core.Models.DTOs
{
	public class AddOrderItemDTO
	{
		public Guid? OrderId { get; set; }
		public string? ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
	}

	public static class OrderItemAddRequestDTOExtensions
	{
		public static OrderItem ToOrderItem(this AddOrderItemDTO dto) => new ()
		{
			ProductName = dto.ProductName,
			Quantity = dto.Quantity,
			UnitPrice = dto.UnitPrice,
			TotalPrice = dto.Quantity * dto.UnitPrice
		};

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
