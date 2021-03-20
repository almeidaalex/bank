using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Contracts
{
    public interface IAccount
    {
        int No { get; }

        Result Withdraw(decimal amount); //TODO: is temporary
    }
}
