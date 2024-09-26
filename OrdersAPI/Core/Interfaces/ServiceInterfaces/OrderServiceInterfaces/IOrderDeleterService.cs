namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces
{
	public interface IOrderDeleterService
	{
		public Task<bool> DeleteOrderByIdAsync(Guid orderId);
	}
}
