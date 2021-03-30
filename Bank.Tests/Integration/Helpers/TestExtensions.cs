using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Bank.Domain.SeedWork;
using Bank.Infra;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Tests.Integration.Helpers
{
    internal static class TestExtensions
    {
        internal static string AsJson<T>(this T model) where T : class =>        
            JsonSerializer.Serialize(model);
        
    }
}
