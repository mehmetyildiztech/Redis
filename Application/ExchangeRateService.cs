namespace Application
{
    public class ExchangeRateService(IExchangeRateProvider provider, ICacheService cache) : IExchangeRateService
    {
        /// <summary>
        /// Cache for one hour
        /// </summary>
        private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(1);

        /// <summary>
        /// Gets exchange rates from redis
        /// If it is empty in redis, fetches via http call from the source
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        public async Task<ExchangeRates> GetLatestAsync(string baseCurrency, CancellationToken cancellationToken)
        {
            var currency = baseCurrency.ToUpperInvariant();
            var key = $"rates:{currency}";

            var cached = await cache.GetAsync<ExchangeRates>(key);
            if (cached is not null) return cached;

            var rates = await provider.GetLatestAsync(currency, cancellationToken);

            await cache.SetAsync(key, rates, CacheDuration);
            return rates;
        }
    }
}
