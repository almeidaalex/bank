using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Contracts
{
    public interface IYieldAccount : IAccount
    {
        DateTime LastYieldedDate { get; }

        void SetBalance(decimal balance, DateTime currentDate);
    }
}
