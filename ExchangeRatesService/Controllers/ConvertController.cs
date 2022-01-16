using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace ExchangeRatesService.Controllers
{
    [Route("api/convert")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        private readonly IOpenExchangeRates _exchangeRates;
        private readonly ILogger<ConvertController> _logger;

        public ConvertController(IOpenExchangeRates exchangeRates, ILogger<ConvertController> logger)
        {
	        _exchangeRates = exchangeRates;
            _logger = logger;
        }

        // GET api/convert?amount=30.2&from=MXN&to=BGN
        [HttpGet()]
        public IActionResult Get(double amount, string from, string to)
        {
            ObjectResult result;
            try
            {
                var rates = _exchangeRates.GetExchangeRates();

                var fromKvp = rates.FirstOrDefault(x => x.Key == from);
                if (!string.IsNullOrEmpty(fromKvp.Key))
                {
                    var toKvp = rates.FirstOrDefault(x => x.Key == to);
                    if (!string.IsNullOrEmpty(toKvp.Key))
                    {
                        result = Ok((toKvp.Value / fromKvp.Value) * amount);
                    }
                    else
                    {
                        result = NotFound($"To currency '{to}' does not exist.");
                    }
                }
                else
                {
                    result = NotFound($"From currency '{from}' does not exist.");
                }
            }
            catch (Exception ex)
            {
                var errorMsg = $"Cannot get exchange rate.";
                _logger.LogError(errorMsg, ex);
                result = Problem(errorMsg);
            }

            return result;
        }
    }
}
