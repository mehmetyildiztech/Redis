# Redis Cache POC

ASP.NET Core (.NET 10) API that fetches exchange rates from [Frankfurter](https://frankfurter.dev) and caches them in Redis for one hour, following onion architecture.

## Architecture

```
Application      Core contracts and use cases, no dependencies
  ExchangeRateService        cache-aside: Redis → Frankfurter → store for 1h
  ICacheService, IExchangeRateProvider, IExchangeRateService, ExchangeRates

Infrastructure   → Application
  RedisCacheService          ICacheService over StackExchange.Redis
  FrankfurterExchangeRateProvider   IExchangeRateProvider over HttpClient
  AddInfrastructure()        DI registration

Web              → Application, Infrastructure
  ExchangeRatesController
  Program.cs                 composition root
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Redis, e.g. via Docker:

  ```
  docker run -d --name redis-demo -p 6370:6379 redis
  ```

## Configuration

| Key | Where | Default |
|---|---|---|
| `ConnectionStrings:Redis` | `appsettings.Development.json` | `localhost:6370` |
| `Frankfurter:BaseUrl` | `appsettings.json` | `https://api.frankfurter.dev/` |

Override per environment with `appsettings.{Environment}.json` or environment variables (`ConnectionStrings__Redis`, `Frankfurter__BaseUrl`).

## Run

```
dotnet run --project Web
```

## Usage

```
GET http://localhost:5279/exchangerates?baseCurrency=EUR
```

```json
{
  "base": "EUR",
  "date": "2026-10-23",
  "rates": { "USD": 1.08, "SEK": 11.42 }
}
```

The first request per currency calls Frankfurter; subsequent requests within an hour are served from Redis under the key `rates:{CURRENCY}`. Sample requests are in `Web/Web.http`.
