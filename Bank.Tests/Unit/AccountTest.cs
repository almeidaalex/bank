using Bank.Domain;
using Bank.Domain.Events;
using Bank.Domain.SeedWork;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Bank.Tests.Unit
{
    public class AccountTest
    {
        private readonly Owner _owner;
        public AccountTest()
        {
            _owner = new Owner("Alex A.");
        }
        
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
            var account = new Account(_owner, 1, 200);            
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
                .OfType<DepositedAmountEvent>()
                .Should().ContainSingle(e => e.Amount == amount);
        }

        [Fact]
        public void Should_add_event_domain_withdrawn_when_withdraw_is_called()
        {
            var amount = 200.14m;            
            var account = new Account(_owner, 1, 8000);
            account.Withdraw(amount);
            account.Events
                .OfType<WithdrawnAmountEvent>()
                .Should().ContainSingle(e => e.Amount == -200.14m);
        }

        [Fact]
        public void Should_not_add_event_domain_withdrawn_when_withdraw_fail()
        {
            var amount = 200.14m;
            var account = new Account(_owner, 1, 100);
            account.Withdraw(amount);
            account.Events.Should().BeEmpty();
        }

        [Fact]
        public void Should_add_event_domain_charged_when_charge_payment_is_called()
        {   
            var account = new Account(_owner, 1, 1000);
            var invoice = new Invoice(34566, new DateTime(), 500);
            account.ChargePayment(invoice);
            account.Events
                .OfType<ChargedPaymentEvent>()
                .Should().ContainSingle(e => e.Amount == -500);
              
        }
    }
}
