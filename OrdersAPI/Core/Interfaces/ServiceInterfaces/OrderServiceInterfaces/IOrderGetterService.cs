using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces
{
	public interface IOrderGetterService
	{
		public Task<List<OrderResponseDTO>> GetAllOrdersAsync();
		public Task<OrderResponseDTO?> GetOrderByIdAsync(Guid id);
	}
}
