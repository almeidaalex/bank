using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Bank.Api;
using Bank.Api.DTOs;
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
            var owner = new Owner(1, "Alex A.");
            var account = new Account(owner, accountNo: 1, initialBalance: 10000);            
            _httpClient = factory.AddSeedData(account).CreateClient();
        }


        [Fact]
        public async Task Should_withdrawn_money_succesfully()
        {
            var command = new WithdrawCommand { AccountNo = 1, Amount = 599 };
            var content = new StringContent(command.AsJson(), Encoding.UTF8, "application/json" );
            var response = await _httpClient.PostAsync("api/account/withdraw", content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task Should_return_error_for_invalid_amounts(decimal amount)
        {
            var command = new WithdrawCommand { AccountNo = 2, Amount = amount };
            var content = new StringContent(command.AsJson(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/withdraw", content);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_add_account_history_after_succesfull_withdraw()
        {
            var command = new WithdrawCommand { AccountNo = 1, Amount = 599 };
            var content = new StringContent(command.AsJson(), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("api/account/withdraw", content);
            var response = await _httpClient.GetAsync("api/account/1/statement");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var account = await response.Content.ReadFromJsonAsync<AccountDto>();

            account.Statements.Should().HaveCount(1);
            var statement = account.Statements.First();
            statement.Amount.Should().Be(-599);            
        }
    }
}
