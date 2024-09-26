using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Services.OrderServices
{
	public class OrderGetterService : IOrderGetterService
	{
		#region private readonly fields
		private readonly IOrdersRepository _ordersRepository;
        #endregion

		# region constructors
        public OrderGetterService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
		# endregion

		/// <summary>
		/// Retrieves all orders.
		/// </summary>
		/// <returns>A list of OrderResponseDTOs</returns>
		public async Task<List<OrderResponseDTO>> GetAllOrdersAsync()
		{
			var orders = await _ordersRepository.GetAllOrdersAsync();
			return orders.ToOrderResponseDTOList();
		}

		/// <summary>
		/// Finds an order by the given GUID.
		/// </summary>
		/// <param name="orderId"></param>
		/// <returns>A nullable OrderResponseDTO.</returns>
		public async Task<OrderResponseDTO?> GetOrderByIdAsync(Guid orderId)
		{
			Order? order = await _ordersRepository.GetOrderByIdAsync(orderId);
			return order?.ToOrderResponseDTO();
		}
	}
}
