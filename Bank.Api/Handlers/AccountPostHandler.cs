using System.Threading;
using System.Threading.Tasks;

using Bank.Api.Commands;
using Bank.Domain;
using Bank.Domain.Contracts;
using Bank.Infra;

using MediatR;
using MediatR.Pipeline;

namespace Bank.Api.Handlers
{
  public class AccountPostHandler<TRequest, TResponse> :
      IRequestPostProcessor<TRequest, TResponse>
      where TResponse : Result<Account>
  {
    private readonly BankDbContext _context;

    public AccountPostHandler(BankDbContext context)
    {
      this._context = context;
    }

    public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
      if (response.Success)
        _context.SaveChangesAsync(cancellationToken);
      return Task.CompletedTask;
    }
  }
}