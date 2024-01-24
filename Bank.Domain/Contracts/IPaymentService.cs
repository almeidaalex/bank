namespace Bank.Domain.Contracts
{
  public interface IPaymentService
  {
    Result Pay(IPaybleAccount account, Invoice invoice);
  }
}