using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bank.Api;
using Bank.Api.Commands;
using Bank.Domain;
using Bank.Tests.Integration.Fixtures;
using Bank.Tests.Integration.Helpers;
using FluentAssertions;
using Xunit;

namespace Bank.Tests.Integration
{
    [Collection(FixtureNames.ACCOUNT_FIXTURE)]
    public class DepositMoneyTest : IClassFixture<DefaultAccountFixture>
    {
        private readonly HttpClient _httpClient;
        public DepositMoneyTest(AccountFixture<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Should_be_possible_to_deposit_amount_on_account()
        {
            var command = new DepositCommand { AccountNo = 4, Amount = 100 };
            var content = new StringContent(command.AsJson(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/deposit", content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
