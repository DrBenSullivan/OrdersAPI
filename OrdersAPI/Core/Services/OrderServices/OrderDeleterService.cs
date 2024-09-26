﻿using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces;

namespace OrdersAPI.Core.Services.OrderServices
{
	public class OrderDeleterService : IOrderDeleterService
	{
		#region private readonly fields
		private readonly ILogger<OrderDeleterService> _logger;
		private readonly IOrdersRepository _ordersRepository;
        #endregion

        #region constructors
        public OrderDeleterService(ILogger<OrderDeleterService> logger, IOrdersRepository ordersRepository)
        {
            _logger = logger;
			_ordersRepository = ordersRepository;
        }
		#endregion

		public async Task<bool> DeleteOrderByIdAsync(Guid orderId)
		{
			_logger.LogInformation("{Method} reached with OrderId {OrderId}... Calling {NextMethod}", nameof(DeleteOrderByIdAsync), orderId, nameof(_ordersRepository.DeleteOrderByIdAsync));

			bool isDeleted = await _ordersRepository.DeleteOrderByIdAsync(orderId);

			if (isDeleted) _logger.LogInformation("Order with OrderId {OrderId} successfully deleted.", orderId);
			else _logger.LogInformation("Order with OrderId {OrderId} not found.", orderId);

			return isDeleted;
		}
	}
}