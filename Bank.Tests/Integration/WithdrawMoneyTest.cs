using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
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
        private readonly HttpClient _httpClient;
        public WithdrawMoneyTest(ApplicationFactoryMemoryDb<Startup> factory)
        {     
            _httpClient = factory.AddSeedData(new Account(accountNo: 1, initialBalance: 1000)).CreateClient();
        }


        [Fact]
        public async Task Should_withdrawn_money_succesfully()
        {
            var model = new WithdrawViewModel { AccountNo = 1, Amount = 599 };
            var content = new StringContent(model.AsJson(), Encoding.UTF8, "application/json" );
            var response = await _httpClient.PostAsync("api/account/withdraw", content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task Should_return_error_for_invalid_amounts(decimal amount)
        {
            var model = new WithdrawViewModel { AccountNo = 2, Amount = amount };
            var content = new StringContent(model.AsJson(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/withdraw", content);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
       
    }
}
