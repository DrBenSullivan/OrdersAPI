using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Services.OrderItemsServices
{
	///<inheritdoc/>
	public class OrderItemUpdaterService : IOrderItemUpdaterService
	{
		#region private readonly fields
		private readonly ILogger<OrderItemUpdaterService> _logger;
		private readonly IOrderItemsRepository _orderItemsRepository;
        #endregion

        #region constructors
		///<inheritdoc/>
        public OrderItemUpdaterService(ILogger<OrderItemUpdaterService> logger, IOrderItemsRepository orderItemsRepository)
        {
            _logger = logger;
			_orderItemsRepository = orderItemsRepository;
        }
		#endregion

		///<inheritdoc/>
		public async Task<OrderItemResponseDTO?> UpdateOrderItemAsync(UpdateOrderItemDTO updateOrderItemDTO)
		{
			_logger.LogInformation("{Service}.{Method} reached with OrderId {OrderId}. Calling {NextClass}.{NextMethod}.", 
				nameof(OrderItemUpdaterService), nameof(UpdateOrderItemAsync), updateOrderItemDTO.OrderId, nameof(_orderItemsRepository), nameof(_orderItemsRepository.UpdateOrderItemAsync));
			
			OrderItem orderItem = updateOrderItemDTO.ToOrderItem();

			OrderItem? updatedOrderItem = await _orderItemsRepository.UpdateOrderItemAsync(orderItem);

			return updatedOrderItem == null
				? null 
				: updatedOrderItem.ToOrderItemResponseDTO();
		}
	}
}
