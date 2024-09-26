using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces
{
	public interface IOrderUpdaterService
	{
		public Task<OrderResponseDTO> UpdateOrderAsync(UpdateOrderDTO updateOrderDTO);
	}
}
