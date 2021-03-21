namespace Bank.Domain.SeedWork
{
    public interface IPaymentService
    {
        Result Pay(IPaybleAccount account, Invoice invoice);
    }
}
