using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Web.Controllers
{
	public class OrdersController : CustomControllerBase
	{
		#region private readonly fields
		private readonly IOrderGetterService _orderGetterService;
        #endregion

        #region constructors
        public OrdersController(IOrderGetterService orderGetterService)
        {
            _orderGetterService = orderGetterService;
        }
		#endregion

		[HttpGet]
		public async Task<ActionResult<List<OrderResponseDTO>>> GetOrders()
		{ 
			return await _orderGetterService.GetAllOrdersAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<OrderResponseDTO>> GetOrderById(Guid id)
		{
			OrderResponseDTO? order = await _orderGetterService.GetOrderByIdAsync(id);

			return order == null
				? Problem($"No order with ID {id} was found.", statusCode: 404)
				: order;
		}

		[]
	}
}
