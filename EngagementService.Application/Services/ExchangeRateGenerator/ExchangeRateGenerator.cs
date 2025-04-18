using System.Collections.Immutable;
using CurrencyConverter.DataAccess;
using CurrencyConverter.Domain;
using ExchangeRate.WebService.Contracts;

namespace UserEngagement.Application.Services.ExchangeRateGenerator;

internal sealed class ExchangeRateGenerator : IExchangeRateGenerator
{
    private ICurrencyRepository CurrencyRepository { get; }

    public ExchangeRateGenerator(ICurrencyRepository currencyRepository)
    {
        CurrencyRepository = currencyRepository;
    }

    public async Task<IImmutableList<CurrencyExchangeRate>> ComposeCurrencyExchangeRatesAsync(string baseCurrencyCode,
        IImmutableList<CurrencyRate> currencyRates)
    {
        Dictionary<string, Currency> currencies = (await CurrencyRepository.GetCurrenciesAsync())
            .ToDictionary(currency => currency.Code.ToLower(), currency => currency);

        if (!currencies.TryGetValue(baseCurrencyCode.ToLower(), out Currency? baseCurrency))
            throw BaseCurrencyNotFoundException(baseCurrencyCode);

        return currencyRates
            .Where(rate => currencies.ContainsKey(rate.CurrencyCode.ToLower()))
            .Select(rate => new CurrencyExchangeRate
            {
                Date = rate.Date,
                CurrencyId = currencies[rate.CurrencyCode.ToLower()].Id,
                BaseCurrencyId = baseCurrency.Id,
                Rate = rate.ExchangeRate
            })
            .ToImmutableList();
    }

    private Exception BaseCurrencyNotFoundException(string baseCurrencyCode)
        => new($"Base currency {baseCurrencyCode} not found.");
}