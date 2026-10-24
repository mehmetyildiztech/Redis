namespace Application
{
    public interface IExchangeRateService
    {
        /// <summary>
        /// Gets latest from cache first then from provider
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ExchangeRates> GetLatestAsync(string baseCurrency, CancellationToken cancellationToken);
    }
}
