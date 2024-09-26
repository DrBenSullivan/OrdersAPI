namespace OrdersAPI.Core.Models.DTOs
{
	public class OrderAddRequestDTO
	{
		public string? OrderId { get; set; }
		public string? CustomerName { get; set; }
		public decimal TotalPrice { get; set; }
		public List<OrderItemAddRequestDTO> OrderItems { get; set; } = new List<OrderItemAddRequestDTO>();
	}
}
