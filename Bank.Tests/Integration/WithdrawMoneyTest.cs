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
    public class WithdrawMoneyTest : IClassFixture<ApplicationFactoryMemoryDb<Startup>> 
    {
        private readonly ApplicationFactoryMemoryDb<Startup> _factory;
        private readonly HttpClient _httpClient;
        public WithdrawMoneyTest(ApplicationFactoryMemoryDb<Startup> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }


        [Fact]
        public async Task Should_withdrawn_money_succesfully()
        {
            var client = _factory.AddSeedData(new Account(accountNo:1, initialBalance: 1000)).CreateClient();

            var model = new WithdrawViewModel { AccountNo = 1, Amount = 599 };
            var content = new StringContent(model.AsJson(), Encoding.UTF8, "application/json" );
            var response = await client.PostAsync("api/account/withdraw", content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
