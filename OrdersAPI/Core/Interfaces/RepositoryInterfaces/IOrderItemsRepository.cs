using OrdersAPI.Core.Models;

namespace OrdersAPI.Core.Interfaces.RepositoryInterfaces
{
	public interface IOrderItemsRepository
	{
		public Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);
	}
}
