namespace Application
{
    public interface IExchangeRateProvider
    {
        /// <summary>
        /// Gets exchange rates for currency via http request from frankfurt exchange
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ExchangeRates> GetLatestAsync(string baseCurrency, CancellationToken cancellationToken);
    }
}
