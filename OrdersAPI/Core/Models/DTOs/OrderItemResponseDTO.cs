using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrdersAPI.Core.Models.DTOs
{
	public class OrderItemResponseDTO
	{
		public Guid OrderItemId { get; set; }
		public Guid OrderId { get; set; }
		public string? ProductName { get; set; }
		public int? Quantity { get; set; }
		public decimal? UnitPrice { get; set; }
		public decimal? TotalPrice { get; set; }
	}
}
