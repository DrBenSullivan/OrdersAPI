using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.WebAPI.Controllers
{
	public class OrdersController : CustomControllerBase
	{
		#region private readonly fields
		private readonly ILogger<OrdersController> _logger;
		private readonly IOrderGetterService _orderGetterService;
		private readonly IOrderAdderService _orderAdderService;
		private readonly IOrderUpdaterService _orderUpdaterService;
        #endregion

        #region constructors
        public OrdersController(ILogger<OrdersController> logger, IOrderGetterService orderGetterService, IOrderAdderService orderAdderService, IOrderUpdaterService orderUpdaterService)
        {
			_logger = logger;
            _orderGetterService = orderGetterService;
			_orderAdderService = orderAdderService;
			_orderUpdaterService = orderUpdaterService;
        }
		#endregion

		/// <summary>
		/// Gets all orders.
		/// </summary>
		/// <returns>A list of orders.</returns>
		[HttpGet]
		public async Task<ActionResult<List<OrderResponseDTO>>> GetOrders()
		{
			LogAction("Getting orders from OrderGetterService...");
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
			LogAction($"Getting order with ID {orderId} from OrderGetterService...");
			OrderResponseDTO? order = await _orderGetterService.GetOrderByIdAsync(orderId);

			if (order == null)
			{
				_logger.LogInformation($"Order with Id {orderId} not found.");
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
			LogAction($"Sending order with OrderNumber {addOrderRequest.OrderNumber} to OrderAdderService...");

			OrderResponseDTO? addedOrder = await _orderAdderService.AddOrderAsync(addOrderRequest);

            if (addedOrder == null)
            {
                _logger.LogWarning($"Order with orderNumber {addOrderRequest.OrderNumber} could not be added to the database...");
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

			LogAction($"Sending updated details for order with ID ${orderId} to OrderUpdaterService...");
			OrderResponseDTO? updatedOrder = await _orderUpdaterService.UpdateOrderAsync(updateOrderRequest);

			if (updatedOrder == null)
			{
				_logger.LogWarning($"Order with ID {orderId} could not be updated...");
				return Problem("An unknown error occurred, please try again later.");
			}

			return Ok(updatedOrder);
		}

		private void LogAction(string message, [System.Runtime.CompilerServices.CallerMemberName] string actionName = "")
		{
			_logger.LogInformation($"{nameof(OrdersController)}.{actionName} reached.\n{message}\n\n");
		}
	}
}
