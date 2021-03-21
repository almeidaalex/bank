namespace Bank.Domain.SeedWork
{
    public interface IPaybleAccount
    {
        bool CanCharge(Invoice invoice);

        void ChargePayment(Invoice invoice);
    }
}
