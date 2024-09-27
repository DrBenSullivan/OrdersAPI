namespace OrdersAPI.Core.Models.DTOs
{
	public class AddOrderDTO
	{
		public string? OrderNumber { get; set; }
		public string? CustomerName { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal TotalPrice { get; set; }
		public List<AddOrderItemDTO> OrderItems { get; set; } = [];
	
		public Order ToOrder() => new ()
		{
			OrderNumber = OrderNumber,
			CustomerName = CustomerName,
			OrderDate = OrderDate,
			TotalPrice = TotalPrice
		};
	}
}
