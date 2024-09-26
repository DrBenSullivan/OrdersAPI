using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Services.OrderServices
{
	public class OrderAdderService : IOrderAdderService
	{
		#region private readonly fields
		private readonly IOrdersRepository _ordersRepository;
		private readonly IOrderItemsRepository _orderItemsRepository;
        #endregion

        #region constructors
        public OrderAdderService(IOrdersRepository ordersRepository, IOrderItemsRepository orderItemsRepository)
        {
            _ordersRepository = ordersRepository;
			_orderItemsRepository = orderItemsRepository;
        }
		#endregion

		public async Task<OrderResponseDTO> AddOrderAsync(AddOrderDTO orderAddRequest)
		{
			Order newOrder = orderAddRequest.ToOrder();
			newOrder.OrderId = Guid.NewGuid();
			var addedOrder = await _ordersRepository.AddOrderAsync(newOrder);
			var addedOrderResponse = addedOrder.ToOrderResponse();

			List<OrderItem> items = orderAddRequest.OrderItems.ToOrderItemsList();
			foreach (var item in items)
			{
				item.OrderItemId = Guid.NewGuid();
				item.OrderId = newOrder.OrderId;
				var addedOrderItem = await _orderItemsRepository.AddOrderItemAsync(item);
				addedOrderResponse.OrderItems.Add(addedOrderItem.ToOrderItemResponse());
			}
			
			return addedOrderResponse;
		}
	}
}
