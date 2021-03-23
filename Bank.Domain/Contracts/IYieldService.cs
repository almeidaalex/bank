using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Domain.Contracts
{
    public interface IYieldService
    {
        void CalculateInterestFor(DateTime currentDate, IYieldAccount account, double interestRate, uint days = 1);
        void CalculateInterestFor(DateTime currentDate, IEnumerable<IYieldAccount> accounts, double interestRate, CancellationToken cancellationToken,  uint days = 1);
    }
}
