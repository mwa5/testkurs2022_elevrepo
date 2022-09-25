namespace Lab3WebApi.Services
{
    public interface IShipmentService
    {
        Task ShipAsync(IAddressInfo info, IEnumerable<ICartItem> items);
    }
}