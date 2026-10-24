using Application;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRatesController(IExchangeRateService exchangeRateService) : ControllerBase
    {
        [HttpGet]
        public Task<ExchangeRates> Get(string baseCurrency = "EUR", CancellationToken cancellationToken = default) =>
            exchangeRateService.GetLatestAsync(baseCurrency, cancellationToken);
    }
}
