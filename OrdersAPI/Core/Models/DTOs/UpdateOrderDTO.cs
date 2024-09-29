#pragma warning disable 1591

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace OrdersAPI.Core.Models.DTOs
{
	public class UpdateOrderDTO
	{
		[Required(ErrorMessage = "The Order ID must be provided.")]
		public Guid OrderId { get; set; }


		[Required(ErrorMessage = "The Order Number must be provided")]
		public string? OrderNumber { get; set; }


		[MaxLength(50, ErrorMessage = "The Customer Name must not exceed 50 characters.")]
		[Required(ErrorMessage = "The Customer Name must be provided.")]
		public string? CustomerName { get; set; }


		[Required(ErrorMessage = "The Order Date must be provided.")] 
		public DateTime OrderDate { get; set; }


		[Precision(18,2)]
		[Range(0, (double) decimal.MaxValue, ErrorMessage = "The Total Cost of the order must not be negative.")]
		[Required(ErrorMessage = "The Total Cost of the order must be provided.")]
		public decimal TotalPrice { get; set; }

		public Order ToOrder() => new ()
		{
			OrderId = OrderId,
			OrderNumber = OrderNumber,
			CustomerName = CustomerName,
			OrderDate = OrderDate,
			TotalPrice = TotalPrice
		};
	}
}
