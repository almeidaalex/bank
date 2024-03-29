﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bank.Domain.Contracts;

namespace Bank.Domain
{
  public class YieldService : IYieldService
  {
    public void CalculateInterestFor(DateTime forDate, IYieldAccount account, double interestRate, uint days = 1)
    {
      if (HasPositiveBalance(account)
          && ItHasAlreadyBeenCalculated(forDate, account.LastYieldedDate.GetValueOrDefault(), days))
      {
        decimal calc = Convert.ToDecimal(Math.Pow(1 + interestRate / 100d, days / 252d) - 1);
        var balance = Math.Round(calc * account.Balance, 2);
        account.SetYield(balance, forDate);
      }
    }

    public void CalculateInterestFor(DateTime forDate, IEnumerable<IYieldAccount> accounts, double interestRate, CancellationToken cancellationToken, uint days = 1)
    {
      var paralleOptions = new ParallelOptions { CancellationToken = cancellationToken };

      Parallel.ForEach(accounts, paralleOptions, account =>
          this.CalculateInterestFor(forDate, account, interestRate, days));
    }

    private static bool ItHasAlreadyBeenCalculated(DateTime forDate, DateTime lastYieldedDate, uint days) =>
        forDate > lastYieldedDate.AddDays(days);

    private static bool HasPositiveBalance(IYieldAccount account) => account.Balance > 0;
  }
}