using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.WebAPI.Controllers
{
	public class OrdersController : CustomControllerBase
	{
		#region private readonly fields
		private readonly IOrderGetterService _orderGetterService;
		private readonly IOrderAdderService _orderAdderService;
        #endregion

        #region constructors
        public OrdersController(IOrderGetterService orderGetterService, IOrderAdderService orderAdderService)
        {
            _orderGetterService = orderGetterService;
			_orderAdderService = orderAdderService;
        }
		#endregion

		/// <summary>
		/// Gets all orders.
		/// </summary>
		/// <returns>A list of orders.</returns>
		[HttpGet]
		public async Task<ActionResult<List<OrderResponseDTO>>> GetOrders()
		{ 
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
			OrderResponseDTO? order = await _orderGetterService.GetOrderByIdAsync(orderId);

			return order == null
				? NotFound($"No order with ID {orderId} was found.")
				: order;
		}

		/// <summary>
		/// Adds a new order.
		/// </summary>
		/// <param name="addOrderRequest">Details of the order.</param>
		/// <returns>The added order.</returns>
		[HttpPost]
		public async Task<ActionResult> AddOrder([Bind] AddOrderDTO addOrderRequest)
		{
			OrderResponseDTO? order = await _orderAdderService.AddOrderAsync(addOrderRequest);
			
			return order == null
				? Problem("An error occurred", statusCode: 500)
				: CreatedAtAction(nameof(GetOrderById), new { orderId = order.OrderId }, order);	
		}
	}
}
