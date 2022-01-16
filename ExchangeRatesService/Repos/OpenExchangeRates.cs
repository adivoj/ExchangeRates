using System;
using System.Collections.Generic;
using ExchangeRatesService.Models;
using RestSharp;

namespace ExchangeRatesService.Repositories
{
	public class OpenExchangeRates : IOpenExchangeRates
	{
		private const string APP_ID = "98ebb3f807f242a18a05a76cbc7b0b89";
		private readonly RestClient _client = new RestClient("https://openexchangerates.org/");

		Dictionary<string, double> IOpenExchangeRates.GetExchangeRates()
		{
			var request = new RestRequest($"api/latest.json?app_id={APP_ID}");
			var query = _client.ExecuteGetAsync<OpenExchangeRatesResult>(request);
			query.Wait();
			var queryResult = query.Result;

			if (queryResult.IsSuccessful)
			{
				return queryResult.Data.rates;
			}
			else
			{
				//log error here?
				throw new Exception(queryResult.ErrorMessage);
			}
		}
	}
}