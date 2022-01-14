using System.Collections.Generic;

namespace ExchangeRatesService
{
	public interface IOpenExchangeRates
	{
		Dictionary<string, decimal> GetExchangeRates();
	}
}