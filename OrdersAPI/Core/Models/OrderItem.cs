﻿#pragma warning disable 1591

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrdersAPI.Core.Models
{
	public class OrderItem
	{
		[Key]
		public Guid OrderItemId { get; set; }


		[ForeignKey("Order")]
		[Required(ErrorMessage = "The Order ID must be provided.")]
		public Guid OrderId { get; set; }
		
		[Required]
		public Order? Order { get; set; }


		[MaxLength(50)]
		[Required(ErrorMessage = "The Product Name must be provided.")]
		public string? ProductName { get; set; }


		[Range(0, int.MaxValue, ErrorMessage = "The Quantity must not be a negative number.")]
		[Required(ErrorMessage = "The Quantity must be provided.")]
		public int Quantity { get; set; }


		[Precision(18,2)]
		[Range(0, (double) decimal.MaxValue, ErrorMessage = "The Unit Price must not be negative.")]
		[Required(ErrorMessage = "The Unit Price must be provided.")]
		public decimal UnitPrice { get; set; }


		[Precision(18,2)]
		[Range(0, (double) decimal.MaxValue, ErrorMessage = "The Total Price must not be negative.")]
		[Required(ErrorMessage = "The Total Price must be provided.")]
		public decimal TotalPrice { get; set; }
	}
}
