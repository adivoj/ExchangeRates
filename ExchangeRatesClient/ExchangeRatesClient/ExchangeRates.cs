using RestSharp;
using System.Threading.Tasks;

namespace ExchangeRatesClient
{
    public class ExchangeRates
    {
        private readonly RestClient _client = new RestClient("https://localhost:44338/");

        public async Task<ExchangeRateResult> GetExchangeRate(decimal amount, string from, string to)
        {
            var request = new RestRequest($"api/convert?amount={amount}&from={from}&to={to}");
            var queryResult = await _client.ExecuteGetAsync(request);

            var result = new ExchangeRateResult();
            if (queryResult.IsSuccessful)
            {
                result.Rate = decimal.Parse(queryResult.Content);
            }
            else
            {
                result.Error = queryResult.Content ?? queryResult.ErrorMessage;
            }

            return result;
        }
    }
}
