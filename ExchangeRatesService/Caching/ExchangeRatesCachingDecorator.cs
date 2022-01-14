using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ExchangeRatesService.Repositories.Caching
{
    public class ExchangeRatesCachingDecorator : IOpenExchangeRates
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<ExchangeRatesCachingDecorator> _logger;
        private readonly IOpenExchangeRates _repository;

        public ExchangeRatesCachingDecorator(IMemoryCache memoryCache, ILogger<ExchangeRatesCachingDecorator> logger, IOpenExchangeRates repository)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            _repository = repository;
        }

        public Dictionary<string, decimal> GetExchangeRates()
        {
            var key = "Rates";
            var rates = _memoryCache.Get<Dictionary<string, decimal>>(key);

            if (rates == null)
            {
                rates = _repository.GetExchangeRates();

                if (rates != null)
                {
                    _logger.LogTrace("Setting ExchangeRates in cache for {CacheKey}", key);
                    _memoryCache.Set(key, rates, TimeSpan.FromMinutes(20));
                }
            }
            else
            {
                _logger.LogTrace("Cache hit for {CacheKey}", key);
            }

            return rates;
        }
    }
}
