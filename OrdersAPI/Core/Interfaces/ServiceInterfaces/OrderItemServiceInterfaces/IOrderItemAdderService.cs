using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces
{
	public interface IOrderItemAdderService
	{
		public Task<OrderItemResponseDTO?> AddOrderItemAsync(AddOrderItemDTO addOrderItemDTO);
	}
}
