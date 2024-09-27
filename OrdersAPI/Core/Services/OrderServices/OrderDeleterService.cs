using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces;
using OrdersAPI.Core.Services.OrderItemsServices;

namespace OrdersAPI.Core.Services.OrderServices
{
	///<inheritdoc/>
	public class OrderDeleterService : IOrderDeleterService
	{
		#region private readonly fields
		private readonly ILogger<OrderDeleterService> _logger;
		private readonly IOrdersRepository _ordersRepository;
        #endregion

        #region constructors
		///<inheritdoc/>
		public OrderDeleterService(ILogger<OrderDeleterService> logger, IOrdersRepository ordersRepository)
        {
            _logger = logger;
			_ordersRepository = ordersRepository;
        }
		#endregion

		///<inheritdoc/>
		public async Task<bool> DeleteOrderByIdAsync(Guid orderId)
		{
			_logger.LogInformation("{Service}.{Method} reached with OrderId {OrderId}... Calling {NextMethod}", nameof(OrderDeleterService), nameof(DeleteOrderByIdAsync), orderId, nameof(_ordersRepository.DeleteOrderByIdAsync));

			bool isDeleted = await _ordersRepository.DeleteOrderByIdAsync(orderId);

			if (isDeleted) _logger.LogInformation("Order with OrderId {OrderId} successfully deleted.", orderId);
			else _logger.LogInformation("Order with OrderId {OrderId} not found.", orderId);

			return isDeleted;
		}
	}
}
