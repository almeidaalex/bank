using Bank.Domain.Contracts;


namespace Bank.Domain
{
    public class PaymentService : IPaymentService
    {
        public Result Pay(IPaybleAccount account, Invoice invoice)
        {
            if (account.CanCharge(invoice))
            {
                account.ChargePayment(invoice);
                return Result.Ok();
            }
            return Result.Fail("Não foi possível realizar o pagamento");
        }
    }
}
