using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces;
using OrdersAPI.Core.Models.DTOs;
using OrdersAPI.Core.Services.OrderItemsServices;

namespace OrdersAPI.WebAPI.Controllers
{
	[Route("api/orders/{orderId}/items")]
	public class OrderItemsController : CustomControllerBase
	{
		#region private readonly fields
		private readonly ILogger<OrderItemsController> _logger;
		private readonly IOrderItemGetterService _orderItemsGetterService;
		private readonly IOrderItemAdderService _orderItemsAdderService;
		private readonly IOrderItemUpdaterService _orderItemsUpdaterService;
		private readonly IOrderItemDeleterService _orderItemDeleterService;
        #endregion

		#region constructors
		public OrderItemsController(ILogger<OrderItemsController> logger, IOrderItemGetterService orderItemsGetterService, IOrderItemAdderService orderItemsAdderService, IOrderItemUpdaterService orderItemsUpdaterService, IOrderItemDeleterService orderItemDeleterService)
		{
			_logger = logger;
			_orderItemsGetterService = orderItemsGetterService;
			_orderItemsAdderService = orderItemsAdderService;
			_orderItemsUpdaterService = orderItemsUpdaterService;
			_orderItemDeleterService = orderItemDeleterService;
		 }
		#endregion

		/// <summary>
		/// Gets all OrderItems from a given Order.
		/// </summary>
		/// <param name="orderId">The GUID of the Order to retrieve OrderItems for.</param>
		/// <returns>A List of OrderItemResponseDTOs of the OrderItems if the Order exists. Otherwise, 404 Not Found.</returns>
		[HttpGet]
		public async Task<ActionResult<List<OrderItemResponseDTO>>> GetAllItemsByOrderId(Guid orderId)
		{
			_logger.LogInformation("{Controller}.{Method} reached with orderId {OrderId}, calling {NextMethod}", nameof(OrderItemsController), nameof(GetAllItemsByOrderId), orderId, nameof(_orderItemsGetterService.GetOrderItemsByOrderIdAsync));

			List<OrderItemResponseDTO>? orderItems = await _orderItemsGetterService.GetOrderItemsByOrderIdAsync(orderId);

			if (orderItems == null || orderItems.Count == 0) 
			{
				return NotFound();
			}

			else return orderItems;
		}


		/// <summary>
		/// Gets an OrderItem by its GUID.
		/// </summary>
		/// <param name="orderItemId">The GUID of the item to be found.</param>
		/// <returns>An OrderItemResponseDTO of the OrderItem if it exists. Otherwise, 404 Not Found.</returns>
		[HttpGet("{orderItemId}")]
		public async Task<ActionResult<OrderItemResponseDTO>> GetOrderItemById(Guid orderItemId)
		{
			_logger.LogInformation("{Controller}.{Method} reached with orderItemId {OrderItemId}, calling {NextMethod}", nameof(OrderItemsController), nameof(GetOrderItemById), orderItemId, nameof(_orderItemsGetterService.GetOrderItemByIdAsync));

			OrderItemResponseDTO? orderItem = await _orderItemsGetterService.GetOrderItemByIdAsync(orderItemId);

			if (orderItem == null)
			{
				return NotFound(); 
			}

			else return orderItem;
		}


		/// <summary>
		/// Adds and OrderItem to a given Order.
		/// </summary>
		/// <param name="orderId">The GUID of the Order to be added to.</param>
		/// <param name="addOrderItemDTO">An AddOrderItemDTO of the OrderItem to add to the Order.</param>
		/// <returns>An OrderItemResponseDTO of the added item, if sucessful.</returns>
		[HttpPost]
		public async Task<ActionResult<OrderItemResponseDTO>> AddItemToOrder(Guid orderId, [Bind] AddOrderItemDTO addOrderItemDTO)
		{
			if (orderId == Guid.Empty) return BadRequest("No Order ID provided in the URL.");
			if (addOrderItemDTO.OrderId == Guid.Empty) return BadRequest("No Order ID provided in the request body.");
			if (orderId != addOrderItemDTO.OrderId)	return BadRequest("Order IDs do not match. Order ID in route must match the Order ID of the item to be added.");
			
			_logger.LogInformation("{Controller}.{Method} reached with orderId {OrderId}, calling {NextMethod}.", nameof(OrderItemsController), nameof(AddItemToOrder), orderId, nameof(_orderItemsAdderService.AddOrderItemAsync));

			OrderItemResponseDTO? addedOrderItemDTO = await _orderItemsAdderService.AddOrderItemAsync(addOrderItemDTO);

			if (addedOrderItemDTO == null)
			{
				return Problem();
			}
			
			return CreatedAtAction(nameof(GetOrderItemById), new { orderId = addedOrderItemDTO.OrderId, orderItemId = addedOrderItemDTO.OrderItemId }, addedOrderItemDTO);
		}


		/// <summary>
		/// Updates an existing OrderItem's details.
		/// </summary>
		/// <param name="orderId">The OrderId of the given OrderItem.</param>
		/// <param name="orderItemId">The OrderItemId of the given OrderItem.</param>
		/// <param name="updateOrderItemDTO">An updateOrderItemDTO of the OrderItem to be updated.</param>
		/// <returns></returns>
		[HttpPut("{orderItemId}")]
		public async Task<ActionResult<OrderItemResponseDTO>> UpdateOrderItem(Guid orderId, Guid orderItemId, [Bind] UpdateOrderItemDTO updateOrderItemDTO)
		{
			if (orderId == Guid.Empty) return BadRequest("A valid OrderId must be provided.");
			if (orderItemId == Guid.Empty) return BadRequest("A valid OrderItemId must be provided.");
			if (orderId != updateOrderItemDTO.OrderId) return BadRequest("The OrderId in the Url and the OrderId of the UpdateOrderItemDTO must match exactly.");
			if (orderItemId != updateOrderItemDTO.OrderItemId) return BadRequest("The OrderItemId in the Url and the OrderItemId of the UpdateOrderItemDTO must match exactly.");

			_logger.LogInformation("{Controller}.{Method} reached with OrderId {OrderId} and OrderItemId {OrderItemId}. Calling {NextClass}.{NextMethod}.", nameof(OrderItemsController), nameof(UpdateOrderItem), orderId, orderItemId, nameof(_orderItemsUpdaterService), nameof(_orderItemsUpdaterService.UpdateOrderItemAsync));

			OrderItemResponseDTO? updatedOrderItemDTO = await _orderItemsUpdaterService.UpdateOrderItemAsync(updateOrderItemDTO);

			if (updatedOrderItemDTO == null) {
				return Problem();
			}

			return Ok(updatedOrderItemDTO);
		}

		/// <summary>
		/// Deletes the OrderItem with the given OrderItemId from the database.
		/// </summary>
		/// <param name="orderItemId">The OrderItemId of the order to be deleted.</param>
		/// <returns>StatusCode 202 if successful, StatusCode 404 if not.</returns>
		[HttpDelete("{orderItemId}")]
		public async Task<ActionResult> DeleteOrderItemById(Guid orderItemId)
		{
			_logger.LogInformation("{Controller}.{Method} reached with Order ID {OrderId}... Calling {NextClass}.{NextMethod}", nameof(OrderItemsController), nameof(DeleteOrderItemById), orderItemId, nameof(OrderItemDeleterService), nameof(OrderItemDeleterService.DeleteOrderItemAsync));

			var isDeleted = await _orderItemDeleterService.DeleteOrderItemAsync(orderItemId);

			if (isDeleted) return NoContent();
			else return NotFound();
		}
	}
}
