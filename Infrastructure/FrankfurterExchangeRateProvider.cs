using Application;
using System.Net.Http.Json;

namespace Infrastructure
{
    public class FrankfurterExchangeRateProvider(HttpClient httpClient) : IExchangeRateProvider
    {
        public async Task<ExchangeRates> GetLatestAsync(string baseCurrency, CancellationToken cancellationToken) =>
            (await httpClient.GetFromJsonAsync<ExchangeRates>($"v1/latest?base={baseCurrency}", cancellationToken))!;
    }
}
