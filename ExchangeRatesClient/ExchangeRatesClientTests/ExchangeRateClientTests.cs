using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExchangeRatesClient;

namespace ExchangeRatesClientTests
{
	[TestClass]
	public class ExchangeRateClientTests
	{
		[TestMethod]
		public void TestCorrectRate()
		{
			var result = new ExchangeRates().GetExchangeRate(30, "MXN", "BGN").Result;
			Assert.IsNull(result.Error, "There shouldn't be any errors.");
			Assert.AreEqual("2.52", result.Rate.ToString("0.00"), "Rate is not correct.");
		}

		[TestMethod]
		public void TestInvalidFromCurrency()
		{
			var result = new ExchangeRates().GetExchangeRate(30, "BUL", "BGN").Result;
			Assert.AreEqual(0, result.Rate, "Rate should not be set.");
			Assert.AreEqual("\"From currency 'BUL' does not exist.\"", result.Error);
		}

		[TestMethod]
		public void TestInvalidToCurrency()
		{
			var result = new ExchangeRates().GetExchangeRate(30, "MXN", "AT").Result;
			Assert.AreEqual(0, result.Rate, "Rate should not be set.");
			Assert.AreEqual("\"To currency 'AT' does not exist.\"", result.Error);
		}

		/* NOTE: Stop the service before running this test */
		//[TestMethod]
		//public void TestServiceDown()
		//{
		//	var result = new ExchangeRates().GetExchangeRate(30, "MXN", "BGN").Result;
		//	Assert.AreEqual(0, result.Rate, "Rate should not be set.");
		//	Assert.AreEqual("An error occurred while sending the request.", result.Error, "Stop the service before running this test");
		//}
	}
}
