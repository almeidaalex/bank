using System;
using System.Diagnostics;
using System.Linq;
using Bank.Api;
using Bank.Domain;
using Bank.Infra;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Bank.Tests.Integration.Fixtures
{

   

    [CollectionDefinition(FixtureNames.ACCOUNT_FIXTURE)]
    public class DefaultDatabaseCollection : ICollectionFixture<AccountFixture<Startup>>        
    {
        
    }

    public class AccountFixture<TStartup> : WebApplicationFactory<TStartup>, IDisposable
        where TStartup : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => {
                var descriptor = services.SingleOrDefault(
                       d => d.ServiceType == typeof(DbContextOptions<BankDbContext>));

                services.Remove(descriptor);

                var dbName = "bank_db_" + Guid.NewGuid();
                services.AddDbContext<BankDbContext>(options => {
                    options.UseMySQL($"Server=localhost;port=3307;Database={dbName};Uid=root;Pwd=b@nk1;");
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<BankDbContext>();                               
                db.Database.Migrate();
                db.SaveChanges();

            });
        }

        internal void RemoveAccounts(int id)
        {
            var scope = this.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<BankDbContext>();
            var account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            var scope = this.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<BankDbContext>();
            db.Database.EnsureDeleted();
            base.Dispose(disposing);
        }

        public void AddMoreAccounts(params Account[] accounts)
        {
            var scope = this.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<BankDbContext>();
            db.Accounts.AddRange(accounts);
            db.SaveChanges();
        }
    }
}
