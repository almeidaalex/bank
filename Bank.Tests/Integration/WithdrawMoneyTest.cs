using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Bank.Api;
using Bank.Api.DTOs;
using Bank.Domain;
using Bank.Tests.Integration.Fixtures;
using Bank.Tests.Integration.Helpers;
using FluentAssertions;
using Xunit;

namespace Bank.Tests.Integration
{
    [Collection("AccountFixture")]
    public class WithdrawMoneyTest : IClassFixture<AccountFixtures<Startup>> 
    {   
        private readonly HttpClient _httpClient;
        public WithdrawMoneyTest(AccountFixtures<Startup> factory)
        {        
            _httpClient = factory.CreateDefaultClient();
        }


        [Fact]
        public async Task Should_withdrawn_money_succesfully()
        {
            var command = new WithdrawCommand { AccountNo = 3, Amount = 599 };
            var content = new StringContent(command.AsJson(), Encoding.UTF8, "application/json" );
            var response = await _httpClient.PostAsync("api/account/withdraw", content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task Should_return_error_for_invalid_amounts(decimal amount)
        {
            var command = new WithdrawCommand { AccountNo = 3, Amount = amount };
            var content = new StringContent(command.AsJson(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/withdraw", content);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_add_account_history_after_succesfull_withdraw()
        {
            var command = new WithdrawCommand { AccountNo = 3, Amount = 599 };
            var content = new StringContent(command.AsJson(), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("api/account/withdraw", content);
            var response = await _httpClient.GetAsync("api/account/3/statement");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var account = await response.Content.ReadFromJsonAsync<AccountDto>();

            account.Statements.Should().HaveCount(1);
            var statement = account.Statements.First();
            statement.Amount.Should().Be(-599);            
        }
    }
}
