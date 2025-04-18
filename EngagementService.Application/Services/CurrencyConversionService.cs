using System.Diagnostics.CodeAnalysis;
using CurrencyConverter.CoreAccess;
using CurrencyConverter.DataAccess;
using CurrencyConverter.Domain;

namespace UserEngagement.Application.Services;

public class CurrencyConversionService : ICurrencyConversionService
{
    private ICurrencyExchangeRateRepository CurrencyExchangeRateRepository { get; }

    public CurrencyConversionService(ICurrencyExchangeRateRepository currencyExchangeRateRepository)
    {
        CurrencyExchangeRateRepository = currencyExchangeRateRepository;
    }

    public async Task<CurrencyConversion> GetCurrencyConversionAsync(long currencyExchangeRateId, decimal amount)
    {
        CurrencyExchangeRate? currencyExchangeRate =
            await CurrencyExchangeRateRepository.GetCurrencyExchangeRateByIdAsync(currencyExchangeRateId);

        if (IsCurrencyExchangeRateNotFound(currencyExchangeRate))
            throw NoExchangeRateFoundException(currencyExchangeRateId);

        return ConvertCurrencyAmount(currencyExchangeRate, amount);
    }

    private bool IsCurrencyExchangeRateNotFound([NotNullWhen(false)] CurrencyExchangeRate? currencyExchangeRate)
        => currencyExchangeRate == null;

    private static Exception NoExchangeRateFoundException(long currencyExchangeRateId)
        => new($"No exception currency rate was found by id {currencyExchangeRateId}");

    private CurrencyConversion ConvertCurrencyAmount(CurrencyExchangeRate currencyExchangeRate, decimal amount)
        => new()
        {
            CurrencyExchangeRateId = currencyExchangeRate.Id,
            ExchangeRateDate = currencyExchangeRate.Date,
            Amount = amount,
            ConvertedAmount = ConvertAmount(amount, currencyExchangeRate.Rate),
            Rate = currencyExchangeRate.Rate
        };

    private decimal ConvertAmount(decimal amount, decimal conversionRate)
        => amount * conversionRate;
}