using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Services.OrderServices
{
	public class OrderAdderService : IOrderAdderService
	{
		#region private readonly fields
		private readonly ILogger<OrderAdderService> _logger;
		private readonly IOrdersRepository _ordersRepository;
		private readonly IOrderItemsRepository _orderItemsRepository;
        #endregion

        #region constructors
        public OrderAdderService(ILogger<OrderAdderService> logger, IOrdersRepository ordersRepository, IOrderItemsRepository orderItemsRepository)
        {
			_logger = logger;
            _ordersRepository = ordersRepository;
			_orderItemsRepository = orderItemsRepository;
        }
		#endregion

		/// <summary>
		/// Adds the given order.
		/// </summary>
		/// <param name="addOrderDTO">AddOrderDTO to be added.</param>
		/// <returns>OrderResponseDTO of the added order.</returns>
		public async Task<OrderResponseDTO> AddOrderAsync(AddOrderDTO addOrderDTO)
		{
			_logger.LogInformation("{Method} reached... Processing AddOrderDTO with OrderNumber {OrderNumber}", nameof(OrderAdderService.AddOrderAsync), addOrderDTO.OrderNumber);

			Order newOrder = addOrderDTO.ToOrder();
			newOrder.OrderId = Guid.NewGuid();

			_logger.LogInformation("GUID for new order: {OrderId}... Calling {NextMethod}", newOrder.OrderId, nameof(_ordersRepository.AddOrderAsync));
			var addedOrder = await _ordersRepository.AddOrderAsync(newOrder);
			var addedOrderResponse = addedOrder.ToOrderResponseDTO();

			List<OrderItem> items = addOrderDTO.OrderItems.ToOrderItemsList();
			foreach (var item in items)
			{
				item.OrderItemId = Guid.NewGuid();
				item.OrderId = newOrder.OrderId;
				var addedOrderItem = await _orderItemsRepository.AddOrderItemAsync(item);
				addedOrderResponse.OrderItems.Add(addedOrderItem.ToOrderItemResponseDTO());
			}
			
			return addedOrderResponse;
		}
	}
}
