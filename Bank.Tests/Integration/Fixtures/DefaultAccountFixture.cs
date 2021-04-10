using System;
using System.Threading.Tasks;
using Bank.Api;
using Bank.Domain;
using MySql.Data.MySqlClient;
using Respawn;

namespace Bank.Tests.Integration.Fixtures
{
    internal class DefaultAccountFixture : IDisposable
    {
        private readonly AccountFixture<Startup> _accountFixture;
        private readonly Checkpoint _checkpoint;

        public DefaultAccountFixture(AccountFixture<Startup> accountFixture)
        {
            _accountFixture = accountFixture;

            _checkpoint = new Checkpoint {
                DbAdapter = DbAdapter.MySql,
                TablesToIgnore = new[] { "Accounts", "Owner" }
            };

            var account = new Account(new Owner(4, "Alex A."), 4, 10000);
            _accountFixture.AddMoreAccounts(account);
        }

        public void Dispose()
        {
            var connection = _accountFixture.GetDbConnection();
            connection.Open();
            Task.WaitAll(_checkpoint.Reset(connection));
        }


    }
}
