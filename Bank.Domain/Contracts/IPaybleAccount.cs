namespace Bank.Domain.Contracts
{
    public interface IPaybleAccount
    {
        bool CanCharge(Invoice invoice);

        void ChargePayment(Invoice invoice);
    }
}
