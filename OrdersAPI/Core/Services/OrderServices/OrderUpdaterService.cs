using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Services.OrderServices
{
	public class OrderUpdaterService : IOrderUpdaterService
	{
		#region private readonly fields
		private readonly ILogger<OrderUpdaterService> _logger;
		private readonly IOrdersRepository _ordersRepository;
        #endregion

        #region constructors
        public OrderUpdaterService(ILogger<OrderUpdaterService> logger, IOrdersRepository ordersRepository)
        {
			_logger = logger;
			_ordersRepository = ordersRepository;   
        }
		#endregion

		/// <summary>
		/// Updates the Order with the given UpdateOrderDTO's OrderId.
		/// </summary>
		/// <param name="updateOrderDTO">The Order details to be updated.</param>
		/// <returns>An OrderResponseDTO with the updated details or, null if fails.</returns>
		public async Task<OrderResponseDTO?> UpdateOrderAsync(UpdateOrderDTO updateOrderDTO)
		{
			_logger.LogInformation("{Service}.{Method} reached for OrderNumber {OrderNumber}. Calling {NextClass}.{NextMethod}.", nameof(OrderUpdaterService), nameof(UpdateOrderAsync), updateOrderDTO.OrderNumber, nameof(_ordersRepository), nameof(_ordersRepository.UpdateOrderAsync));
			Order updatedOrderDetails = updateOrderDTO.ToOrder();
			
			Order? updatedOrder = await _ordersRepository.UpdateOrderAsync(updatedOrderDetails);
			return updatedOrder?.ToOrderResponseDTO();
		}
	}
}
