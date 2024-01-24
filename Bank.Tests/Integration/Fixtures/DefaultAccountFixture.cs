using System;
using System.Threading.Tasks;
using Bank.Api;
using Bank.Domain;
using Respawn;

namespace Bank.Tests.Integration.Fixtures
{
    internal class DefaultAccountFixture : IAsyncDisposable
    {
        private readonly AccountFixture<Startup> _accountFixture;
        
        public DefaultAccountFixture(AccountFixture<Startup> accountFixture)
        {
            _accountFixture = accountFixture;

            var account = new Account(new Owner(4, "Alex A."), 4, 10000);
            _accountFixture.AddMoreAccounts(account);
        }

        public async ValueTask DisposeAsync()
        {
            var connection = _accountFixture.GetDbConnection();

            var checkpoint = await Respawner.CreateAsync(connection, new RespawnerOptions {
                DbAdapter = DbAdapter.MySql,
                TablesToIgnore = new Respawn.Graph.Table[] { "Accounts", "Owner" }
            });

            await checkpoint.ResetAsync(connection);
        }
    }
}
