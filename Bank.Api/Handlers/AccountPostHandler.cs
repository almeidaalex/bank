using Bank.Domain;
using Bank.Infra;

using MediatR;
using MediatR.Pipeline;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Api.Handlers
{
  public class AccountPostHandler<TRequest, TResponse> :
      IRequestPostProcessor<TRequest, TResponse>
      where TResponse : Result<Account>
      where TRequest : IRequest<TResponse>
  {
    private readonly BankDbContext _context;

    public AccountPostHandler(BankDbContext context)
    {
      this._context = context;
    }

    public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
      if (response.Success)
        await _context.SaveChangesAsync(cancellationToken);
    }
  }
}
