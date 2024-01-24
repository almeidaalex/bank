using System;
using System.Linq;
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

using Xunit;
using Xunit.Abstractions;

namespace Bank.Tests.Integration
{
  [Collection(FixtureNames.ACCOUNT_FIXTURE)]
  public class PaymentInvoiceTest
  {

    private readonly HttpClient _httpClient;
    private readonly ITestOutputHelper _testOutput;

    public PaymentInvoiceTest(AccountFixture<Startup> factory, ITestOutputHelper testOutput)
    {
      _httpClient = factory.CreateDefaultClient();
      _testOutput = testOutput;
      _testOutput.WriteLine($"The constructor was called {DateTime.Now}");
    }

    [Fact]
    public async Task Should_pay_an_invoice_succesfully()
    {
      var invoice = new InvoiceDto
      {
        Number = 345454,
        DueDate = new DateTime(),
        Amount = 500
      };

      var command = new PaymentCommand { AccountNo = 1001, Invoice = invoice };
      var content = new StringContent(command.AsJson(), Encoding.UTF8, "application/json");
      var response = await _httpClient.PostAsync("api/account/payment", content);

      response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_and_a_historical_registry_when_a_payment_charged_successfuly()
    {

      var invoice = new InvoiceDto
      {
        Number = 345454,
        DueDate = new DateTime(),
        Amount = 600
      };

      var command = new PaymentCommand { AccountNo = 1001, Invoice = invoice };
      var content = new StringContent(command.AsJson(), Encoding.UTF8, "application/json");
      await _httpClient.PostAsync("api/account/payment", content);

      var response = await _httpClient.GetAsync("api/account/1001/statement");
      response.StatusCode.Should().Be(HttpStatusCode.OK);

      var account = await response.Content.ReadFromJsonAsync<AccountDto>();

      var statement = account.Statements.First(s => s.Amount == -600);

    }


    [Fact]
    public async Task Should_return_bad_request_when_try_to_pay_a_negative_amount()
    {
      var invoice = new InvoiceDto
      {
        Number = 345454,
        DueDate = new DateTime(),
        Amount = -100
      };

      var command = new PaymentCommand { AccountNo = 1001, Invoice = invoice };
      var content = new StringContent(command.AsJson(), Encoding.UTF8, "application/json");

      var response = await _httpClient.PostAsync("api/account/payment", content); ;
      response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

    }
  }
}
