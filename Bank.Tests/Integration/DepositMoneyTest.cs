using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bank.Api;
using Bank.Api.Commands;
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
            var owner = new Owner("Alex A.");
            _httpClient = factory.AddSeedData(new Account(owner, accountNo: 2, initialBalance: 1000)).CreateClient();
        }

        [Fact]
        public async Task Should_be_possible_to_deposit_amount_on_account()
        {
            var command = new DepositCommand { AccountNo = 2, Amount = 100 };
            var content = new StringContent(command.AsJson(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/deposit", content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
