namespace MultiShop.Payment.Services.Abstract
{
    public interface IOrderService
    {
        Task<bool> UpdateOrderPaymentStatusAsync(string basketId, bool isPaid);
    }
}
