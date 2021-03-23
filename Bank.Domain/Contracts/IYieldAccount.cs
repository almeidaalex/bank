using System;

namespace Bank.Domain.Contracts
{
    public interface IYieldAccount : IAccount
    {
        DateTime? LastYieldedDate { get; }

        void SetYield(decimal yield, DateTime currentDate);
    }
}
