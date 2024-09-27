using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.RepositoryInterfaces
{
	public interface IOrderItemsRepository
	{
		public Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);
		public Task<List<OrderItem>?> GetOrderItemsByOrderIdAsync(Guid orderId);
		public Task<OrderItem?> GetOrderItemByIdAsync(Guid orderId);
	}
}
