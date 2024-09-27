using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Services.OrderItemsServices
{
	public class OrderItemsGetterService : IOrderItemsGetterService
	{
		#region private readonly fields
		private readonly ILogger<OrderItemsGetterService> _logger;
		private readonly IOrderItemsRepository _orderItemsRepository;
        #endregion

        #region constructors
        public OrderItemsGetterService(ILogger<OrderItemsGetterService> logger, IOrderItemsRepository orderItemsRepository)
        {
            _logger = logger;
            _orderItemsRepository = orderItemsRepository;
        }
		#endregion

		/// <summary>
		/// Gets a list of OrderItems from an Order by the Order's OrderId.
		/// </summary>
		/// <param name="orderId">The Order's GUID.</param>
		/// <returns>A list of OrderItemResponseDTOs or null, if no OrderItems are found.</returns>
		public async Task<List<OrderItemResponseDTO>?> GetOrderItemsByOrderIdAsync(Guid orderId)
		{
			_logger.LogInformation("{Service}.{Method} reached with OrderId {OrderId}. Calling {NextClass}.{NextMethod}.", 
				nameof(OrderItemsGetterService), nameof(GetOrderItemsByOrderIdAsync), orderId, nameof(_orderItemsRepository), nameof(_orderItemsRepository.GetOrderItemsByOrderIdAsync));

			List<OrderItem>? orderItems = await _orderItemsRepository.GetOrderItemsByOrderIdAsync(orderId);

			if (orderItems == null) 
			{
				_logger.LogWarning("No Order with OrderId {OrderId} exists in the database.", orderId);
				return null;
			}

			_logger.LogInformation("Order with OrderId {OrderId} found, returning result to caller.", orderId);
			return orderItems.ToOrderItemResponseDTOList();
		}

		/// <summary>
		/// Gets an OrderItem by its OrderItemId.
		/// </summary>
		/// <param name="orderItemId">The OrderItem's GUID.</param>
		/// <returns>An OrderItemResponseDTO or, null if the OrderItem does not exist.</returns>
		public async Task<OrderItemResponseDTO?> GetOrderItemByIdAsync(Guid orderItemId)
		{
			_logger.LogInformation("{Service}.{Method} reached with OrderId {OrderId}. Calling {NextClass}.{NextMethod}.", 
				nameof(OrderItemsGetterService), nameof(GetOrderItemByIdAsync), orderItemId, nameof(_orderItemsRepository), nameof(_orderItemsRepository.GetOrderItemByIdAsync));

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
