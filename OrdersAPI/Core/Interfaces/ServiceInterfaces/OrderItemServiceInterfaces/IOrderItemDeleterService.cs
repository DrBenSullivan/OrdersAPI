namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces
{
	public interface IOrderItemDeleterService
	{
		public Task<bool> DeleteOrderItemAsync(Guid orderItemId);
	}
}
