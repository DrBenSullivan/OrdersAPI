using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Services.OrderItemsServices
{
	///	<inheritdoc/>
	public class OrderItemGetterService : IOrderItemGetterService
	{
		#region private readonly fields
		private readonly ILogger<OrderItemGetterService> _logger;
		private readonly IOrderItemsRepository _orderItemsRepository;
        #endregion

        #region constructors
        ///	<inheritdoc/>
		public OrderItemGetterService(ILogger<OrderItemGetterService> logger, IOrderItemsRepository orderItemsRepository)
        {
            _logger = logger;
            _orderItemsRepository = orderItemsRepository;
        }
		#endregion

		///	<inheritdoc/>
		public async Task<List<OrderItemResponseDTO>?> GetOrderItemsByOrderIdAsync(Guid orderId)
		{
			_logger.LogInformation("{Service}.{Method} reached with OrderId {OrderId}. Calling {NextClass}.{NextMethod}.", 
				nameof(OrderItemGetterService), nameof(GetOrderItemsByOrderIdAsync), orderId, nameof(_orderItemsRepository), nameof(_orderItemsRepository.GetOrderItemsByOrderIdAsync));

			List<OrderItem>? orderItems = await _orderItemsRepository.GetOrderItemsByOrderIdAsync(orderId);

			if (orderItems == null) 
			{
				_logger.LogWarning("No Order with OrderId {OrderId} exists in the database.", orderId);
				return null;
			}

			_logger.LogInformation("Order with OrderId {OrderId} found, returning result to caller.", orderId);
			return orderItems.ToOrderItemResponseDTOList();
		}

		///<inheritdoc/>
		public async Task<OrderItemResponseDTO?> GetOrderItemByIdAsync(Guid orderItemId)
		{
			_logger.LogInformation("{Service}.{Method} reached with OrderId {OrderId}. Calling {NextClass}.{NextMethod}.", 
				nameof(OrderItemGetterService), nameof(GetOrderItemByIdAsync), orderItemId, nameof(_orderItemsRepository), nameof(_orderItemsRepository.GetOrderItemByIdAsync));

			OrderItem? orderItem = await _orderItemsRepository.GetOrderItemByIdAsync(orderItemId);

			if (orderItem == null)
			{
				_logger.LogWarning("No OrderItem with OrderItemId {OrderItemId} exists in the database.", orderItemId);
				return null;
			}
			else
			{
				_logger.LogInformation("OrderItem with OrderId {OrderItemId} found, returning result to caller.", orderItemId);
				return orderItem.ToOrderItemResponseDTO();
			}
		}
	}
}
