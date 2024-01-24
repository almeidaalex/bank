using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using Bank.Api;
using Bank.Api.Commands;
using Bank.Api.DTOs;
using Bank.Tests.Integration.Fixtures;
using Bank.Tests.Integration.Helpers;

using FluentAssertions;

using Microsoft.AspNetCore.Http;

using Xunit;

namespace Bank.Tests.Integration
{
  [Collection(FixtureNames.ACCOUNT_FIXTURE)]
  public class CalculateIncomeTest
  {
    private readonly HttpClient _httpClient;
    public CalculateIncomeTest(AccountFixture<Startup> factory)
    {
      _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Should_calculate_income_for_account()
    {
      var command = new CalculateIncomeCommand { ForDate = new DateTime(2021, 01, 01), InterestRate = 2.3 };
      var content = new StringContent(command.AsJson(), Encoding.UTF8, "application/json");
      var response = await _httpClient.PutAsync("/api/account/calculateIncome", content);

      response.StatusCode.Should().Be(HttpStatusCode.Accepted);

      var getResponse = await _httpClient.GetAsync("/api/account/1001/statement");
      var account = await getResponse.Content.ReadFromJsonAsync<AccountDto>();



      account.Statements.Should().HaveCount(1);
    }


  }
}
