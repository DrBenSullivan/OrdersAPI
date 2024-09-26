using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Infrastructure.DatabaseContext;

namespace OrdersAPI.Infrastructure.Repositories
{
	public class OrderItemsRepository : IOrderItemsRepository
	{
		#region private readonly fields
		private readonly ApplicationDbContext _db;
        #endregion

        #region constructors
        public OrderItemsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
		#endregion

		public async Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
		{
			await _db.AddAsync(orderItem);
			await _db.SaveChangesAsync();
			return orderItem;
		}
	}
}
