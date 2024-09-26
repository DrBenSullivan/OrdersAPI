namespace OrdersAPI.Core.Models.DTOs
{
	public class AddOrderDTO
	{
		public string? OrderNumber { get; set; }
		public string? CustomerName { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal TotalPrice { get; set; }
		public List<AddOrderItemDTO> OrderItems { get; set; } = new List<AddOrderItemDTO>();
	}

	public static class OrderAddRequestDTOExtensions
	{
		public static Order ToOrder(this AddOrderDTO dto) => new ()
		{
			OrderNumber = dto.OrderNumber,
			CustomerName = dto.CustomerName,
			OrderDate = dto.OrderDate,
			TotalPrice = dto.TotalPrice
		};
	}
}
