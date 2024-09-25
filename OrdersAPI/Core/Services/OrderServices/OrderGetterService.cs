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
	}
}
