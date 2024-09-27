using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces
{
	public interface IOrderItemUpdaterService
	{
		public Task<OrderItemResponseDTO?> UpdateOrderItemAsync(UpdateOrderItemDTO updateOrderItemDTO);
	}
}
