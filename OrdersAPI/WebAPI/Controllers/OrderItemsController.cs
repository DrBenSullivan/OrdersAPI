using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.WebAPI.Controllers
{
	[ApiController]
	[Route("api/orders/{orderId}/items")]
	public class OrderItemsController : CustomControllerBase
	{
		#region private readonly fields
		private readonly ILogger<OrderItemsController> _logger;
		private readonly IOrderItemsGetterService _orderItemsGetterService;
		private readonly IOrderItemsAdderService _orderItemsAdderService;
        #endregion

		#region constructors
		public OrderItemsController(ILogger<OrderItemsController> logger, IOrderItemsGetterService orderItemsGetterService, IOrderItemsAdderService orderItemsAdderService)
		{
			_logger = logger;
			_orderItemsGetterService = orderItemsGetterService;
			_orderItemsAdderService = orderItemsAdderService;
		 }
		#endregion

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

		[HttpPost]
		public async Task<ActionResult<OrderItemResponseDTO>> AddItemToOrder(Guid orderId, AddOrderItemDTO addOrderItemDTO)
		{
			if (orderId == Guid.Empty) return BadRequest("No Order ID provided in the URL.");
			if (addOrderItemDTO.OrderId == Guid.Empty) return BadRequest("No Order ID provided in the request body.");
			if (orderId != addOrderItemDTO.OrderId)
			{
				return BadRequest("Order IDs do not match. Order ID in route must match the Order ID of the item to be added.");
			}

			_logger.LogInformation("{Controller}.{Method} reached with orderId {OrderId}, calling {NextMethod}.", nameof(OrderItemsController), nameof(AddItemToOrder), orderId, nameof(_orderItemsAdderService.AddOrderItemAsync));

			OrderItemResponseDTO? addedOrderItemDTO = await _orderItemsAdderService.AddOrderItemAsync(addOrderItemDTO);

			if (addedOrderItemDTO == null)
			{
				return NotFound();
			}
			
			return CreatedAtAction(nameof(GetOrderItemById), new { orderId = addedOrderItemDTO.OrderId, orderItemId = addedOrderItemDTO.OrderItemId }, addedOrderItemDTO);
		}
	}
}
