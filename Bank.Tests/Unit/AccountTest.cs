﻿using Bank.Domain;
using Bank.Domain.Events;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bank.Tests.Unit
{
    public class AccountTest
    {
        [Fact]
        public void Should_be_possible_to_deposit_money_on_banking_account()
        {
            var account = new Account(1);
            account.Deposit(100.11m);
            account.Balance.Should().Be(100.11m);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void Should_not_be_possible_to_deposit_zero_or_negative_amount(decimal amount)
        {
            var account = new Account(1);
            var result = account.Deposit(amount);
            result.Failure.Should().BeTrue();
        }

        [Fact]
        public void Should_monetize_the_balance_every_day_with_by_given_rate()
        {
            var account = new Account(1);
            account.Deposit(100.00m);
            for (int day = 1; day <= 10; day++)
            {
                account.Monetize(0.34m);
            }
            Math.Round(account.Balance,2).Should().Be(103.45m);
        }

        [Fact]
        public void Should_be_possible_to_withdraw_money_from_banking_account()
        {
            var account = new Account(1, 200);            
            account.Withdraw(50.0m);

            account.Balance.Should().Be(150.00m);
        }

        [Fact]
        public void Should_add_event_domain_deposited_when_deposit_is_called()
        {
            var amount = 99.11m;
            var account = new Account(1);
            account.Deposit(amount);
            account.Events
                .Should()
                .AllBeAssignableTo<DepositedAmount>()
                .And
                .OnlyHaveUniqueItems(e => e.Amount == amount);
        }

        [Fact]
        public void Should_add_event_domain_withdrawn_when_withdraw_is_called()
        {
            var amount = 200.14m;
            var expected = new WithdrawnAmount(amount);
            var account = new Account(1);
            account.Withdraw(amount);
            account.Events.Should().ContainEquivalentOf(expected);
        }
    }
}
