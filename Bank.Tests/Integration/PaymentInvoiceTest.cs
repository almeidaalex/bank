﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Bank.Api;
using Bank.Api.Commands;
using Bank.Api.DTOs;
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
            var owner = new Owner(1, "Alex A.");
            _httpClient = factory.AddSeedData(new Account(owner, accountNo: 3, initialBalance: 10000)).CreateClient();
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

            var command = new PaymentCommand { AccountNo = 3, Invoice = invoice };
            var content = new StringContent(command.AsJson(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/account/payment", content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_and_a_historical_registry_when_a_payment_charged_successfuly()
        {
            var invoice = new InvoiceDto {
                Number = 345454,
                DueDate = new DateTime(),
                Amount = 600
            };

            var command = new PaymentCommand { AccountNo = 3, Invoice = invoice };
            var content = new StringContent(command.AsJson(), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("api/account/payment", content);
            
            var response = await _httpClient.GetAsync("api/account/3/statement");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var account = await response.Content.ReadFromJsonAsync<AccountDto>();

            var statement = account.Statements.First(s => s.Amount == -600);           
            
        }


        [Fact]
        public async Task Should_return_bad_request_when_try_to_pay_a_negative_amount()
        {
            var invoice = new InvoiceDto {
                Number = 345454,
                DueDate = new DateTime(),
                Amount = -100
            };

            var command = new PaymentCommand { AccountNo = 3, Invoice = invoice };
            var content = new StringContent(command.AsJson(), Encoding.UTF8, "application/json");           

            var response = await _httpClient.PostAsync("api/account/payment", content); ;
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }
    }
}
