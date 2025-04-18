using System.Collections.Immutable;
using CurrencyConverter.CoreAccess;
using CurrencyConverter.DataAccess;
using CurrencyConverter.Domain;

namespace UserEngagement.Application.Services;

public class CurrencyService : ICurrencyService
{
    private ICurrencyRepository CurrencyRepository { get; }

    public CurrencyService(ICurrencyRepository currencyRepository)
    {
        CurrencyRepository = currencyRepository;
    }

    public async Task<IImmutableList<Currency>> GetCurrenciesAsync()
        => await CurrencyRepository.GetCurrenciesAsync();
}