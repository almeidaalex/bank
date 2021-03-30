using System;
using System.Linq;
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
    [CollectionDefinition("AccountFixture")]
    public class AccountFixtures<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => {
                var descriptor = services.SingleOrDefault(
                       d => d.ServiceType == typeof(DbContextOptions<BankDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<BankDbContext>(options => {
                    options.UseInMemoryDatabase("BankDbTest");
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<BankDbContext>();
                var account = new Account(new Owner(1, "Alex A."), accountNo: 3, initialBalance: 10000);
                db.Accounts.Add(account);
                db.SaveChanges();

            });
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            return base.CreateHost(builder);
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
