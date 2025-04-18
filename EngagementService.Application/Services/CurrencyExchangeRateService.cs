using CurrencyConverter.CoreAccess;
using CurrencyConverter.DataAccess;
using CurrencyConverter.Domain;

namespace UserEngagement.Application.Services;

public class CurrencyExchangeRateService : ICurrencyExchangeRateService
{
    private ICurrencyExchangeRateRepository CurrencyExchangeRateRepository { get; }

    public CurrencyExchangeRateService(ICurrencyExchangeRateRepository currencyExchangeRateRepository)
    {
        CurrencyExchangeRateRepository = currencyExchangeRateRepository;
    }

    public async Task<CurrencyExchangeRate?> GetCurrencyExchangeRateByCurrencyAsync(int currencyId,
        int baseCurrencyId, string date)
    {
        if (DateTime.TryParse(date, out DateTime rateDate))
            return await CurrencyExchangeRateRepository.GetCurrencyExchangeRateByCurrencyAsync(currencyId,
                baseCurrencyId, rateDate);

        return await CurrencyExchangeRateRepository.GetLatestCurrencyExchangeRateByCurrencyAsync(currencyId,
            baseCurrencyId);
    }
}