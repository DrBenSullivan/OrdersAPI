using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Services.OrderServices
{
	///<inheritdoc/>
	public class OrderGetterService : IOrderGetterService
	{
		#region private readonly fields
		private readonly ILogger<OrderGetterService> _logger;
		private readonly IOrdersRepository _ordersRepository;
        #endregion

		# region constructors
        ///<inheritdoc/>
		public OrderGetterService(ILogger<OrderGetterService> logger, IOrdersRepository ordersRepository)
        {
			_logger = logger;
            _ordersRepository = ordersRepository;
        }
		# endregion

		///<inheritdoc/>
		public async Task<List<OrderResponseDTO>> GetAllOrdersAsync()
		{
			_logger.LogInformation("{Service}.{Method} reached... \nCalling {NextMethod}...", nameof(OrderGetterService), nameof(GetAllOrdersAsync), nameof(_ordersRepository.GetAllOrdersAsync));
			var orders = await _ordersRepository.GetAllOrdersAsync();
			return orders.ToOrderResponseDTOList();
		}

		///<inheritdoc/>
		public async Task<OrderResponseDTO?> GetOrderByIdAsync(Guid orderId)
		{
			_logger.LogInformation("{Service}.{Method} reached with orderId {OrderId}... \nCalling {NextMethod}", nameof(OrderGetterService), nameof(GetOrderByIdAsync), orderId, nameof(_ordersRepository.GetOrderByIdAsync));
			Order? order = await _ordersRepository.GetOrderByIdAsync(orderId);
			return order?.ToOrderResponseDTO();
		}
	}
}
