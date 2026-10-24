namespace Application
{
    public record ExchangeRates(string Base, DateOnly Date, Dictionary<string, decimal> Rates);
}
