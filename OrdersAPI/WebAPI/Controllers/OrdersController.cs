using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces;
using OrdersAPI.Core.Models.DTOs;
using OrdersAPI.Core.Services.OrderServices;

namespace OrdersAPI.WebAPI.Controllers
{
	public class OrdersController : CustomControllerBase
	{
		#region private readonly fields
		private readonly ILogger<OrdersController> _logger;
		private readonly IOrderGetterService _orderGetterService;
		private readonly IOrderAdderService _orderAdderService;
		private readonly IOrderUpdaterService _orderUpdaterService;
		private readonly IOrderDeleterService _orderDeleterService;
        #endregion

        #region constructors
        public OrdersController(
			ILogger<OrdersController> logger, 
			IOrderGetterService orderGetterService, 
			IOrderAdderService orderAdderService, 
			IOrderUpdaterService orderUpdaterService,
			IOrderDeleterService orderDeleterService)
        {
			_logger = logger;
            _orderGetterService = orderGetterService;
			_orderAdderService = orderAdderService;
			_orderUpdaterService = orderUpdaterService;
			_orderDeleterService = orderDeleterService;
        }
		#endregion

		/// <summary>
		/// Gets all orders.
		/// </summary>
		/// <returns>A list of orders.</returns>
		[HttpGet]
		public async Task<ActionResult<List<OrderResponseDTO>>> GetOrders()
		{
			_logger.LogInformation("Getting orders from OrderGetterService...");
			return await _orderGetterService.GetAllOrdersAsync();
		}


		/// <summary>
		/// Retrieves an order by the given GUID.
		/// </summary>
		/// <param name="orderId">The GUID of the order to be retrieved.</param>
		/// <returns>An order.</returns>
		[HttpGet("{orderId}")]
		public async Task<ActionResult<OrderResponseDTO>> GetOrderById([Bind] Guid orderId)
		{
			_logger.LogInformation("Getting order with ID {OrderId} from OrderGetterService...", orderId);
			OrderResponseDTO? order = await _orderGetterService.GetOrderByIdAsync(orderId);

			if (order == null)
			{
				_logger.LogInformation("Order with Id {OrderId} not found.", orderId);
				return NotFound($"No Order with ID {orderId} could be retrieved from the database.");
			}

			return order;
		}


		/// <summary>
		/// Adds a new order.
		/// </summary>
		/// <param name="addOrderRequest">Details of the order.</param>
		/// <returns>The added order.</returns>
		[HttpPost]
		public async Task<ActionResult> AddOrder([Bind] AddOrderDTO addOrderRequest)
		{
			_logger.LogInformation("Sending order with OrderNumber {OrderNumber} to OrderAdderService...", addOrderRequest.OrderNumber);

			OrderResponseDTO? addedOrder = await _orderAdderService.AddOrderAsync(addOrderRequest);

            if (addedOrder == null)
            {
                _logger.LogWarning("Order with orderNumber {OrderNumber} could not be added to the database...", addOrderRequest.OrderNumber);
				return Problem("An unknown error occurred, please try again later.", statusCode: 500);
            }

            return CreatedAtAction(nameof(GetOrderById), new { orderId = addedOrder.OrderId }, addedOrder);	
		}

		/// <summary>
		/// Updates a give order.
		/// </summary>
		/// <param name="orderId">GUID of the order to be updated in query string.</param>
		/// <param name="updateOrderRequest">UpdateOrderDTO with new information.</param>
		/// <returns>OrderResponseDTO of the updated order.</returns>
		[HttpPut("{orderId}")]
		public async Task<ActionResult<OrderResponseDTO>> UpdateOrder(Guid orderId, [Bind] UpdateOrderDTO updateOrderRequest)
		{
			if (orderId != updateOrderRequest.OrderId)
			{
				return BadRequest($"The ID in query string ({orderId}) does not match that of the update request ({updateOrderRequest.OrderId}). The IDs must match exactly.");
			}

			_logger.LogInformation("Valid details received for order with ID {OrderId}.\nCalling {NextClass}{Method}", orderId.ToString(), nameof(_orderUpdaterService), nameof(_orderUpdaterService.UpdateOrderAsync));
			OrderResponseDTO? updatedOrder = await _orderUpdaterService.UpdateOrderAsync(updateOrderRequest);

			if (updatedOrder == null)
			{
				_logger.LogWarning("Order with ID {OrderId} could not be updated...", orderId);
				return Problem("An unknown error occurred, please try again later.");
			}

			return Ok(updatedOrder);
		}


		/// <summary>
		/// Deletes the order with the given orderId from the database.
		/// </summary>
		/// <param name="orderId">The orderId of the order to be deleted.</param>
		/// <returns>StatusCode 202 if successful, StatusCode 404 if not.</returns>
		[HttpDelete("{orderId}")]
		public async Task<ActionResult> DeleteOrderById(Guid orderId)
		{
			_logger.LogInformation("{Method} reached with Order ID {OrderId}... Calling {NextMethod}", nameof(DeleteOrderById), orderId, nameof(_orderDeleterService.DeleteOrderByIdAsync));

			var isDeleted = await _orderDeleterService.DeleteOrderByIdAsync(orderId);

			if (isDeleted) return NoContent();
			else return NotFound();
		}
	}
}
