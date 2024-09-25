using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace OrdersAPI.Core.Models
{
	public class Order
	{
		[Key]
		public Guid? OrderId { get; set; }



		[Required(ErrorMessage = "The Order Number must be provided")]
		public string? OrderNumber { get; set; }



		[Required(ErrorMessage = "The Customer Name must be provided.")]
		[MaxLength(50, ErrorMessage = "The Customer Name must not exceed 50 characters.")]
		public string? CustomerName { get; set; }


		
		[Required(ErrorMessage = "The Order Date must be provided.")] 
		public DateTime? OrderDate { get; set; }



		[Required(ErrorMessage = "The Total Cost of the order must be provided.")]
		[Precision(18,2)]
		[Range(0, Double.MaxValue, ErrorMessage = "The Total Cost of the order must not be negative.")]
		public decimal? TotalCost { get; set; }
	}
}
