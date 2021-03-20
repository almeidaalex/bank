using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bank.Infra;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Tests.Integration.Helpers
{
    public class ApplicationFactoryMemoryDb<TStartup> : WebApplicationFactory<TStartup> 
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

            });
        }
    }
}
