using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Services.OrderServices
{
	public class OrderUpdaterService : IOrderUpdaterService
	{
		#region private readonly fields
		private readonly IOrdersRepository _ordersRepository;
        #endregion

        #region constructors
        public OrderUpdaterService(IOrdersRepository ordersRepository)
        {
			_ordersRepository = ordersRepository;   
        }
		#endregion

		public async Task<OrderResponseDTO> UpdateOrderAsync(UpdateOrderDTO updateOrderDTO)
		{
			Order updatedOrderDetails = updateOrderDTO.ToOrder();
			
			Order? updatedOrder = await _ordersRepository.UpdateOrderAsync(updatedOrderDetails);
			return updatedOrder?.ToOrderResponseDTO();
		}
	}
}
