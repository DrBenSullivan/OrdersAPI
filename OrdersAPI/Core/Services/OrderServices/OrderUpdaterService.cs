using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Services.OrderServices
{
	///<inheritdoc/>
	public class OrderUpdaterService : IOrderUpdaterService
	{
		#region private readonly fields
		private readonly ILogger<OrderUpdaterService> _logger;
		private readonly IOrdersRepository _ordersRepository;
        #endregion

        #region constructors
        ///<inheritdoc/>
		public OrderUpdaterService(ILogger<OrderUpdaterService> logger, IOrdersRepository ordersRepository)
        {
			_logger = logger;
			_ordersRepository = ordersRepository;   
        }
		#endregion

		///<inheritdoc/>
		public async Task<OrderResponseDTO?> UpdateOrderAsync(UpdateOrderDTO updateOrderDTO)
		{
			_logger.LogInformation("{Service}.{Method} reached for OrderNumber {OrderNumber}. Calling {NextClass}.{NextMethod}.", nameof(OrderUpdaterService), nameof(UpdateOrderAsync), updateOrderDTO.OrderNumber, nameof(_ordersRepository), nameof(_ordersRepository.UpdateOrderAsync));
			Order updatedOrderDetails = updateOrderDTO.ToOrder();
			
			Order? updatedOrder = await _ordersRepository.UpdateOrderAsync(updatedOrderDetails);
			return updatedOrder?.ToOrderResponseDTO();
		}
	}
}
