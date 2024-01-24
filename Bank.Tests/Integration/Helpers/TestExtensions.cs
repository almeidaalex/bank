using System.Text.Json;

namespace Bank.Tests.Integration.Helpers
{
  internal static class TestExtensions
  {
    internal static string AsJson<T>(this T model) where T : class =>
        JsonSerializer.Serialize(model);

  }
}
