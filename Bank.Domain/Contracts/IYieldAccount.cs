using System;

namespace Bank.Domain.Contracts
{
    public interface IYieldAccount : IAccount
    {
        DateTime? LastYieldedDate { get; }

        void SetBalance(decimal balance, DateTime currentDate);
    }
}
