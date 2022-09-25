namespace Lab3WebApi.Services
{
    public interface ICartService
    {
        double Total();
        IEnumerable<ICartItem> Items();
    }
}
