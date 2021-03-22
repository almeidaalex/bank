using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bank.Api;
using Bank.Api.DTOs;
using Bank.Api.ViewModels;
using Bank.Domain;
using Bank.Tests.Integration.Helpers;
using FluentAssertions;
using Xunit;

namespace Bank.Tests.Integration
{
    public class PaymentInvoiceTest : IClassFixture<ApplicationFactoryMemoryDb<Startup>>
    {

        private readonly HttpClient _httpClient;

        public PaymentInvoiceTest(ApplicationFactoryMemoryDb<Startup> factory)
        {
            var owner = new Owner("Alex A.");
            _httpClient = factory.AddSeedData(new Account(owner, accountNo: 3, initialBalance: 1000)).CreateClient();
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

            var model = new PaymentViewModel { AccountNo = 3, Invoice = invoice };
            var content = new StringContent(model.AsJson(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/payment", content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
