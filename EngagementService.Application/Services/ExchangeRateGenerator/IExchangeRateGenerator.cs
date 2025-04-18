using System.Collections.Immutable;
using CurrencyConverter.Domain;
using ExchangeRate.WebService.Contracts;

namespace UserEngagement.Application.Services.ExchangeRateGenerator;

public interface IExchangeRateGenerator
{
    Task<IImmutableList<CurrencyExchangeRate>> ComposeCurrencyExchangeRatesAsync(string baseCurrencyCode,
        IImmutableList<CurrencyRate> currencyRates);
}