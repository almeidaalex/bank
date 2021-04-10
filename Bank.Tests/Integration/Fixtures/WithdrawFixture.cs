using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Api;
using Bank.Domain;
using Bank.Infra;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Tests.Integration.Fixtures
{
    internal class WithdrawFixture : IDisposable
    {
        private readonly AccountFixture<Startup> _accountFixture;

        public WithdrawFixture(AccountFixture<Startup> accountFixture)
        {
            var account = new Account(new Owner(10, "Maria N."), 3, 10000);            
            _accountFixture = accountFixture;
            _accountFixture.AddMoreAccounts(account);
        }


        public void Dispose()
        {   
            _accountFixture.RemoveAccounts(3);
        }
    }
}
