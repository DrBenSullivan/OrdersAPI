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

	}
}
