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
			try
			{
				var orders = await _ordersRepository.GetAllOrdersAsync();
				return orders.ToOrderResponseDTOList();
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		/// <summary>
		/// Finds an order by the given GUID.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>A nullable OrderResponseDTO.</returns>
		public async Task<OrderResponseDTO?> GetOrderByIdAsync(Guid id)
		{
			try
			{
				var order = await _ordersRepository.GetOrderByIdAsync(id);
				return order != null
					? order.ToOrderResponseDTO() 
					: null;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
