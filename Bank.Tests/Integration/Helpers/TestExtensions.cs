using System;
using System.Collections.Generic;
using System.Text.Json;
using Bank.Domain.SeedWork;
using Bank.Infra;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Tests.Integration.Helpers
{
    internal static class TestExtensions
    {
        internal static string AsJson<T>(this T model) where T : struct =>        
            JsonSerializer.Serialize(model);


        internal static WebApplicationFactory<TEntryPoint> AddSeedData<TEntryPoint>(
            this WebApplicationFactory<TEntryPoint> factory, 
            params IEntity[] entities) where TEntryPoint : class
        {
            return factory.WithWebHostBuilder(bd => {
                bd.ConfigureServices(services => {
                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<BankDbContext>();
                    db.AddRange(entities);
                    db.SaveChangesAsync();

                });
            });            
        }
        
    }
}
