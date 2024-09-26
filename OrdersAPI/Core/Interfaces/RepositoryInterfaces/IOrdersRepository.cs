using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.RepositoryInterfaces
{
	public interface IOrdersRepository
	{
		public Task<List<Order>> GetAllOrdersAsync();
		public Task<Order?> GetOrderByIdAsync(Guid orderId);
		public Task<Order> AddOrderAsync(Order order);
		public Task<Order> UpdateOrderAsync(Order order);
	}
}
