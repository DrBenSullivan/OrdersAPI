namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces
{
	/// <summary>
	/// The class with methods to delete OrderItems from the database.
	/// </summary>
	public interface IOrderDeleterService
	{
		/// <summary>
		/// Sends an OrderId to OrderRepository for the Order to be deleted.
		/// </summary>
		/// <param name="orderId">The OrderId of the Order to be deleted.</param>
		/// <returns>If successful, Boolean 'true'. Otherwise, 'false'.</returns>
		public Task<bool> DeleteOrderByIdAsync(Guid orderId);
	}
}
