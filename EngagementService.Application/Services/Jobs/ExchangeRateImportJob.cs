using System.Collections.Immutable;
using CurrencyConverter.Constants;
using CurrencyConverter.DataAccess;
using CurrencyConverter.Domain;
using ExchangeRate.WebService.Contracts;
using ExchangeRate.WebService.DataAccess;
using UserEngagement.Application.Services.ExchangeRateGenerator;

namespace UserEngagement.Application.Services.Jobs;

public sealed class ExchangeRateImportJob : IJob
{
    private ILogger<ExchangeRateImportJob> Logger { get; }
    private ICurrencyExchangeRateRepository CurrencyExchangeRateRepository { get; }
    private IExchangeRateGenerator ExchangeRateGenerator { get; }
    private IExchangeRateWebService ExchangeRateWebService { get; }

    public ExchangeRateImportJob(
        ICurrencyExchangeRateRepository currencyExchangeRateRepository,
        IExchangeRateGenerator exchangeRateGenerator,
        IExchangeRateWebService exchangeRateWebService,
        ILogger<ExchangeRateImportJob> logger)
    {
        CurrencyExchangeRateRepository = currencyExchangeRateRepository;
        ExchangeRateGenerator = exchangeRateGenerator;
        ExchangeRateWebService = exchangeRateWebService;
        Logger = logger;
    }

    public async Task ExecuteAsync()
        => await ProgressAsync();

    private async Task ProgressAsync()
    {
        LogStartOfJob();
        await GetExchangeRates();
        LogEndOfJob();
    }

    private void LogStartOfJob()
        => Logger.LogInformation("The job of currency exchange rates retrieval has started");

    private async Task GetExchangeRates()
    {
        IImmutableList<CurrencyRate> currencyRates = await ExchangeRateWebService.RetrieveCurrencyRatesAsync();

        IImmutableList<CurrencyExchangeRate> currencyExchangeRates =
            await ExchangeRateGenerator.ComposeCurrencyExchangeRatesAsync(
                CurrencyExchangeRates.Currency.BASE_CURRENCY_CODE, currencyRates);

        await CurrencyExchangeRateRepository.CreateCurrencyExchangeRatesAsync(currencyExchangeRates);
    }

    private void LogEndOfJob()
        => Logger.LogInformation("The job to retrieve currency exchange rates has finished");
}