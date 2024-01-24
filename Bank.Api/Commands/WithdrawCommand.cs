using Bank.Api.Commands;
using Bank.Domain;

namespace Bank.Api
{
  public class WithdrawCommand : AccountCommand<Account>
  {
    public decimal Amount { get; set; }
  }
}