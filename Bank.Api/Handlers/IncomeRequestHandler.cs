
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

    public Task<Unit> Handle(CalculateIncomeCommand request, CancellationToken cancellationToken)
    {
      var accounts = this._context.Accounts.ToArray();

      foreach (var account in accounts)
      {
        var yield = _yieldService.CalculateInterestFor(request.ForDate, account, request.InterestRate, days: 1);
        account.SetYield(yield, request.ForDate);
      }

      _context.SaveChanges();
      return Unit.Task;
    }
  }
}
