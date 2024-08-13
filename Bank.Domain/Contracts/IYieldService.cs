using System;

namespace Bank.Domain.Contracts
{
  public interface IYieldService
  {
    decimal CalculateInterestFor(DateTime currentDate, IYieldAccount account, double interestRate, uint days = 1);
  }
}
