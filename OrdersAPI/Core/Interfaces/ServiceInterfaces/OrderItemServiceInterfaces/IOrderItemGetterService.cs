using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces
{
	public interface IOrderItemGetterService
	{
		public Task<List<OrderItemResponseDTO>?> GetOrderItemsByOrderIdAsync(Guid orderId);
		public Task<OrderItemResponseDTO?> GetOrderItemByIdAsync(Guid orderItemId);
	}
}
