using System.Runtime.CompilerServices;

namespace OrdersAPI.Core.Models.DTOs
{
	public class OrderItemAddRequestDTO
	{
		public Guid OrderId { get; set; }
		public string? ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal TotalPrice { get; set; }
	}

	public static class OrderItemAddRequestDTOExtensions
	{
		public static OrderItem ToOrderItem(this OrderItemAddRequestDTO dto)
		{
			return new OrderItem
			{
				OrderId = dto.OrderId,
				ProductName = dto.ProductName,
				Quantity = dto.Quantity,
				UnitPrice = dto.UnitPrice,
				TotalPrice = dto.TotalPrice
			};
		}

		public static List<OrderItem> ToOrderItemList(this List<OrderItemAddRequestDTO> dtos) {
			var orderItems = new List<OrderItem>();
			foreach (var item in dtos)
			{
				orderItems.Add(item.ToOrderItem());
			}
			return orderItems;
		}
	}
}
