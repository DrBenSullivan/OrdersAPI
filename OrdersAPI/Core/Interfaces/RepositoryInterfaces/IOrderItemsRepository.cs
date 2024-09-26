using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.RepositoryInterfaces
{
	public interface IOrderItemsRepository
	{
		public Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);
	}
}
