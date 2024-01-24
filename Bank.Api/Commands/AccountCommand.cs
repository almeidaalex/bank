using Bank.Domain;
using Bank.Domain.Contracts;

using MediatR;

namespace Bank.Api.Commands
{
  public abstract class AccountCommand<TAccount> : IRequest<Result<TAccount>> where TAccount : IAccount
  {
    public int AccountNo { get; set; }
  }
}