using OrdersAPI.Core.Models;

namespace OrdersAPI.Core.Interfaces.RepositoryInterfaces
{
	public interface IOrdersRepository
	{
		public Task<List<Order>> GetAllOrdersAsync();
		public Task<Order?> GetOrderByIdAsync(Guid id);
	}
}
