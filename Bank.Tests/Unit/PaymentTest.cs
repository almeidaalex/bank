using Bank.Domain;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bank.Tests.Unit
{
    public class PaymentTest
    {
        [Fact]
        public void Should_be_able_to_pay_if_the_account_has_enough_balance()
        {
            var service = new PaymentService();
            var account = new Account(2000);            
            decimal amount = 1400.00m;
            Result result = service.Pay(account, amount);
            result.Success.Should().BeTrue();
            account.Balance.Should().Be(600m);
        }

        [Fact]
        public void Should_not_be_allowed_to_pay_negative_number()
        {
            var service = new PaymentService();
            var account = new Account(2000);            
            decimal amount = -1400.00m;
            Result result = service.Pay(account, amount);
            result.Failure.Should().BeTrue();           
        }
    }
}
