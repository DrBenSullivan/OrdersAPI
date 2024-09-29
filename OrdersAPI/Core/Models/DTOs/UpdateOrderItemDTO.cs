#pragma warning disable 1591

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace OrdersAPI.Core.Models.DTOs
{
	public class UpdateOrderItemDTO
	{
		[Required(ErrorMessage = "The Order ID must be provided.")]
		public Guid OrderId { get; set; }


		[Required(ErrorMessage = "The Order Item ID must be provided.")]
		public Guid OrderItemId { get; set; }


		[MaxLength(50)]
		[Required(ErrorMessage = "The Product Name must be provided.")]
		public string? ProductName { get; set; }


		[Range(0, int.MaxValue, ErrorMessage = "The Quantity must not be a negative number.")]
		[Required(ErrorMessage = "The Quantity must be provided.")]
		public int Quantity { get; set; }


		[Precision(18,2)]
		[Range(0, (double) decimal.MaxValue, ErrorMessage = "The Total Price must not be negative.")]
		[Required(ErrorMessage = "The Total Price must be provided.")]
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
