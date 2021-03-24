using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Update;
using Xunit;

namespace Bank.Tests.Integration
{
    public class CalculateIncomeTest : IClassFixture<ApplicationFactoryMemoryDb<Startup>>
    {
        private readonly HttpClient _httpClient;
        public CalculateIncomeTest(ApplicationFactoryMemoryDb<Startup> factory)
        {
            var owner = new Owner(4, "Alex A.");
            _httpClient = factory.AddSeedData(new Account(owner, accountNo: 5, initialBalance: 1000)).CreateClient();
        }

        //[Fact]
        public async Task Should_calculate_income_for_account()
        {
            var command = new CalculateIncomeCommand { ForDate = new DateTime(2021, 01, 01), InterestRate = 2.3 };
            var content = new StringContent(command.AsJson(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/api/account/calculateIncome", content);

            response.StatusCode.Should().Be(HttpStatusCode.Accepted);

            var getResponse = await _httpClient.GetAsync("/api/account/5/statement");
            var account = await getResponse.Content.ReadFromJsonAsync<AccountDto>();

            

            account.Statements.Should().HaveCount(1);
        }      

        
    }
}
