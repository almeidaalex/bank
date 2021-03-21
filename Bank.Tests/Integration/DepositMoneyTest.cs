using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bank.Api;
using Bank.Api.ViewModels;
using Bank.Domain;
using Bank.Tests.Integration.Helpers;
using FluentAssertions;
using Xunit;

namespace Bank.Tests.Integration
{
    public class DepositMoneyTest : IClassFixture<ApplicationFactoryMemoryDb<Startup>>
    {
        private readonly HttpClient _httpClient;
        public DepositMoneyTest(ApplicationFactoryMemoryDb<Startup> factory)
        {
            _httpClient = factory.AddSeedData(new Account(accountNo: 2, initialBalance: 1000)).CreateClient();
        }

        [Fact]
        public async Task Should_be_possible_to_deposit_amount_on_account()
        {
            var model = new DepositViewModel { AccountNo = 2, Amount = 100 };
            var content = new StringContent(model.AsJson(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/deposit", content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
