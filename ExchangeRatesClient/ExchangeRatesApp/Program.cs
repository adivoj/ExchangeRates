using System;
using ExchangeRatesClient;

namespace ExchangeRatesApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var _exchangeRates = new ExchangeRates();

			Console.WriteLine("**** Welcome to Exchange Rate Service ****");
			do
			{
				Console.WriteLine("\r\nEnter: Amount FromCurrency ToCurrency");
				Console.WriteLine("Example: 30 MXN BGN");

				var input = Console.ReadLine().Split(' ');
				if (input.Length == 3)
				{
					var amount = decimal.Parse(input[0]);
					var from = input[1];
					var to = input[2];

					var result = _exchangeRates.GetExchangeRate(amount, from, to).Result;
					if (!string.IsNullOrEmpty(result.Error))
						Console.WriteLine(result.Error);
					else
						Console.WriteLine($"--> For {amount}{from} you get {result.Rate:0.00}{to}");
				}
			} while (true);
		}
	}
}
