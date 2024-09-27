using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Services.OrderItemsServices
{
	public class OrderItemsUpdaterService : IOrderItemsUpdaterService
	{
		#region private readonly fields
		private readonly ILogger<OrderItemsUpdaterService> _logger;
		private readonly IOrderItemsRepository _orderItemsRepository;
        #endregion

        #region constructors
        public OrderItemsUpdaterService(ILogger<OrderItemsUpdaterService> logger, IOrderItemsRepository orderItemsRepository)
        {
            _logger = logger;
			_orderItemsRepository = orderItemsRepository;
        }
		#endregion

		/// <summary>
		/// Updates a given OrderItem.
		/// </summary>
		/// <param name="updateOrderItemDTO">An UpdateOrderItemDTO to update the given OrderItem to.</param>
		/// <returns>An OrderItemResponseDTO if successful. Otherwise, null.</returns>
		public async Task<OrderItemResponseDTO?> UpdateOrderItemAsync(UpdateOrderItemDTO updateOrderItemDTO)
		{
			_logger.LogInformation("{Service}.{Method} reached with OrderId {OrderId}. Calling {NextClass}.{NextMethod}.", 
				nameof(OrderItemsUpdaterService), nameof(UpdateOrderItemAsync), updateOrderItemDTO.OrderId, nameof(_orderItemsRepository), nameof(_orderItemsRepository.UpdateOrderItemAsync));
			
			OrderItem orderItem = updateOrderItemDTO.ToOrderItem();

			OrderItem? updatedOrderItem = await _orderItemsRepository.UpdateOrderItemAsync(orderItem);

			return updatedOrderItem == null
				? null 
				: updatedOrderItem.ToOrderItemResponseDTO();
		}
	}
}
