#pragma warning disable 1591

namespace OrdersAPI.Core.Models.DTOs
{
	public class UpdateOrderDTO
	{
		public Guid OrderId { get; set; }
		public string? OrderNumber { get; set; }
		public string? CustomerName { get; set; }
		public DateTime OrderDate { get; set; }
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
