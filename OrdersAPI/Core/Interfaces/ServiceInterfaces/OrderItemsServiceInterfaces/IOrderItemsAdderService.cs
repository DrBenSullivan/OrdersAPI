using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces
{
	public interface IOrderItemsAdderService
	{
		public Task<OrderItemResponseDTO?> AddOrderItemAsync(AddOrderItemDTO addOrderItemDTO);
	}
}
