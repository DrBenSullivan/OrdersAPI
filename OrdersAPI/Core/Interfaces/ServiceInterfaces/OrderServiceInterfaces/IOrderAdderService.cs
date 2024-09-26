using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces
{
	public interface IOrderAdderService
	{
		public Task<OrderResponseDTO> AddOrderAsync(AddOrderDTO orderAddRequest);

	}
}
