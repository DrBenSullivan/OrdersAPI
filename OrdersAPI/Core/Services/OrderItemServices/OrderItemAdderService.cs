using System.Data.Common;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;
using OrdersAPI.Infrastructure.Repositories;

namespace OrdersAPI.Core.Services.OrderItemsServices
{
	///<inheritdoc/>
	public class OrderItemAdderService : IOrderItemAdderService
	{
		#region private readonly fields
		private readonly ILogger<OrderItemAdderService> _logger;
		private readonly IOrderItemsRepository _orderItemsRepository;
		private readonly IOrdersRepository _ordersRepository;
        #endregion

        #region constructors
		///<inheritdoc/>
        public OrderItemAdderService(ILogger<OrderItemAdderService> logger, IOrderItemsRepository orderItemsRepository, IOrdersRepository ordersRepository)
        {
            _logger = logger;
			_orderItemsRepository = orderItemsRepository;
			_ordersRepository = ordersRepository;
        }
		#endregion

		///<inheritdoc/>
		public async Task<OrderItemResponseDTO?> AddOrderItemAsync(AddOrderItemDTO addOrderItemDTO)
		{
			_logger.LogInformation("{Service}.{Method} reached. Calling {NextMethod}...", nameof(OrderItemAdderService), nameof(AddOrderItemAsync), nameof(_ordersRepository.OrderExistsAsync));
		
			var orderExists = await _ordersRepository.OrderExistsAsync(addOrderItemDTO.OrderId);
			if (!orderExists) return null;

			OrderItem orderItem = addOrderItemDTO.ToOrderItem();
			orderItem.OrderItemId = Guid.NewGuid();
			OrderItem addedOrderItem = await _orderItemsRepository.AddOrderItemAsync(orderItem);

			return addedOrderItem.ToOrderItemResponseDTO();
		}
	}
}
