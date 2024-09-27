﻿using System.Data.Common;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;
using OrdersAPI.Infrastructure.Repositories;

namespace OrdersAPI.Core.Services.OrderItemsServices
{
	public class OrderItemsAdderService : IOrderItemsAdderService
	{
		#region private readonly fields
		private readonly ILogger<OrderItemsAdderService> _logger;
		private readonly IOrderItemsRepository _orderItemsRepository;
		private readonly IOrdersRepository _ordersRepository;
        #endregion

        #region constructors
        public OrderItemsAdderService(ILogger<OrderItemsAdderService> logger, IOrderItemsRepository orderItemsRepository, IOrdersRepository ordersRepository)
        {
            _logger = logger;
			_orderItemsRepository = orderItemsRepository;
			_ordersRepository = ordersRepository;
        }
		#endregion

		/// <summary>
		/// Adds the given AddOrderItemDTO.
		/// </summary>
		/// <param name="addOrderItemDTO">The OrderItem to add.</param>
		/// <returns>An OrderItemResponseDTO of the added OrderItem or, null if fails.</returns>
		public async Task<OrderItemResponseDTO?> AddOrderItemAsync(AddOrderItemDTO addOrderItemDTO)
		{
			_logger.LogInformation("{Service}.{Method} reached. Calling {NextMethod}...", nameof(OrderItemsAdderService), nameof(AddOrderItemAsync), nameof(_ordersRepository.OrderExistsAsync));
		
			var orderExists = await _ordersRepository.OrderExistsAsync(addOrderItemDTO.OrderId);
			if (!orderExists) return null;

			OrderItem orderItem = addOrderItemDTO.ToOrderItem();
			orderItem.OrderItemId = Guid.NewGuid();
			OrderItem addedOrderItem = await _orderItemsRepository.AddOrderItemAsync(orderItem);

			return addedOrderItem.ToOrderItemResponseDTO();
		}
	}
}
