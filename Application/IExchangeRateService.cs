namespace Application
{
    public interface IExchangeRateService
    {
        Task<ExchangeRates> GetLatestAsync(string baseCurrency, CancellationToken cancellationToken);
    }
}
