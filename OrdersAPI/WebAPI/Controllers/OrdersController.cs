using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces;
using OrdersAPI.Core.Models.DTOs;
using OrdersAPI.Core.Services.OrderServices;

namespace OrdersAPI.WebAPI.Controllers
{
	/// <summary>
	/// The Controller for performing operations on the "Orders" table of the application's database.
	/// </summary>
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
        ///<inheritdoc/>
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
		/// Retrieves all orders.
		/// </summary>
		/// <returns>All orders as a list of OrderResponseDTOs.</returns>
		[HttpGet]
		public async Task<ActionResult<List<OrderResponseDTO>>> GetOrders()
		{
			_logger.LogInformation("Getting orders from OrderGetterService...");
			return await _orderGetterService.GetAllOrdersAsync();
		}


		/// <summary>
		/// Retrieves an existing Order using its OrderId.
		/// </summary>
		/// <param name="orderId">The OrderId of the existing Order to be retrieved.</param>
		/// <returns>An Order as an OrderResponseDTO.</returns>
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
		/// Adds a new Order.
		/// </summary>
		/// <param name="addOrderRequest">Details of the Order to be added.</param>
		/// <returns>The added Order as an OrderResponseDTO.</returns>
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
		/// Updates an existing Order.
		/// </summary>
		/// <param name="orderId">OrderId of the existing Order to be updated.</param>
		/// <param name="updateOrderRequest">An UpdateOrderDTO containing the new details of the Order.</param>
		/// <returns>The updated Order as an OrderResponseDTO.</returns>
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
		/// Deletes an existing Order using its OrderId.
		/// </summary>
		/// <param name="orderId">The OrderId of the existing Order to be deleted.</param>
		/// <returns>If successful, StatusCode 204. Otherwise, StatusCode 404.</returns>
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
