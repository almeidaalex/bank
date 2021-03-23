using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Domain;
using Bank.Domain.Contracts;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Xunit;

namespace Bank.Tests.Unit
{
    public class YieldServiceTest
    {
        [Fact]
        public void Should_apply_savings_to_balance_every_day_with_by_given_rate()
        {
            var account = Substitute.For<IYieldAccount>();
            account.Balance.Returns(67.95m);
            var currentDate = new DateTime(2020, 07, 29);
            var yieldService = new YieldService();
            yieldService.CalculateInterestFor(currentDate, account, interestRate: 3.52, days: 1);

            account.Received().SetYield(.01m, currentDate);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_apply_savings_only_to_positive_balance(decimal balance)
        {
            var account = Substitute.For<IYieldAccount>();
            account.Balance.Returns(balance);
            var currentDate = new DateTime(2020, 07, 29);
            var yieldService = new YieldService();
            yieldService.CalculateInterestFor(currentDate, account, interestRate: 3.52, days: 1);

            account.Received(Quantity.None()).SetYield(Arg.Any<decimal>(), Arg.Any<DateTime>());
        }


        [Fact]
        public void Should_not_calculate_interest_rate_twice_for_the_same_day()
        {
            var account = Substitute.For<IYieldAccount>();
            account.Balance.Returns(67.95m);
            account.LastYieldedDate.Returns(new DateTime(2020, 07, 29));

            var currentDate = new DateTime(2020, 07, 29);
            var yieldService = new YieldService();
            yieldService.CalculateInterestFor(currentDate, account, interestRate: 3.52, days: 1);            

            account.Received(Quantity.None()).SetYield(Arg.Any<decimal>(), Arg.Any<DateTime>());
        }

        [Fact]
        public void Should_respect_given_days_to_calculate_the_interest_rate()
        {
            var account = Substitute.For<IYieldAccount>();
            account.Balance.Returns(67.95m);
            account.LastYieldedDate.Returns(new DateTime(2020, 07, 28));

            var currentDate = new DateTime(2020, 07, 29);
            var yieldService = new YieldService();
            yieldService.CalculateInterestFor(currentDate, account, interestRate: 3.52, days: 2);

            account.Received(Quantity.None()).SetYield(Arg.Any<decimal>(), Arg.Any<DateTime>());
        }
    }

    
}
