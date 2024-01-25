
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bank.Api.Commands;
using Bank.Domain.Contracts;
using Bank.Infra;

using MediatR;

namespace Bank.Api.Handlers
{
  public class IncomeRequestHandler : IRequestHandler<CalculateIncomeCommand>
  {
    private readonly BankDbContext _context;
    private readonly IYieldService _yieldService;

    public IncomeRequestHandler(BankDbContext context, IYieldService yieldService)
    {
      _context = context;
      _yieldService = yieldService;
    }

    public async Task<Unit> Handle(CalculateIncomeCommand request, CancellationToken cancellationToken)
    {
      var accounts = this._context.Accounts.ToArray();
      _yieldService.CalculateInterestFor(request.ForDate, accounts, request.InterestRate, cancellationToken, days: 1);
      await _context.SaveChangesAsync(cancellationToken);
      return Unit.Value;
    }
  }
}
