namespace Lab3WebApi.Services
{
    public interface IPaymentService
    {
        Task<bool> ChargeAsync(double total, ICard card);
    }
}