using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Services.OrderItemsServices
{
	public class OrderItemDeleterService : IOrderItemDeleterService
	{
		#region private readonly fields
		private readonly ILogger<OrderItemDeleterService> _logger;
		private readonly IOrderItemsRepository _orderItemsRepository;
        #endregion

        #region constructors
        public OrderItemDeleterService(ILogger<OrderItemDeleterService> logger, IOrderItemsRepository orderItemsRepository)
        {
            _logger = logger;
			_orderItemsRepository = orderItemsRepository;
        }
		#endregion

		/// <summary>
		/// Deletes an OrderItem with the given GUID from the database.
		/// </summary>
		/// <param name="orderItemId">The GUID of the OrderItem to be deleted.</param>
		/// <returns>true if successfully deleted. Otherwise, false.</returns>
		public async Task<bool> DeleteOrderItemAsync(Guid orderItemId)
		{
			_logger.LogInformation("{Service}.{Method} reached with OrderId {OrderId}. Calling {NextClass}.{NextMethod}.", 
				nameof(OrderItemDeleterService), nameof(DeleteOrderItemAsync), orderItemId, nameof(_orderItemsRepository), nameof(_orderItemsRepository.OrderItemExistsAsync));
			
			bool orderItemExists = await _orderItemsRepository.OrderItemExistsAsync(orderItemId);

			if (!orderItemExists) return false;

			bool orderItemDeleted = await _orderItemsRepository.DeleteOrderItemAsync(orderItemId);
			
			return orderItemDeleted;
		}
	}
}
