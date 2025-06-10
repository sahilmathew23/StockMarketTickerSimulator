using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataGenerationEngine;
using StockTicker.Eventing;
using PriceUpdateConsumer;
namespace StockMarketTickerSimulator
{
	internal class Program
	{
		static async Task Main( string[] args )
		{
			UniqueStockSymbols uniqueStockSymbols = new UniqueStockSymbols();
			uniqueStockSymbols.GenerateUniqueStockSymbols();

			StockPriceUpdator stockPriceUpdator = new StockPriceUpdator();
			StockPriceConsumer stockPriceConsumer = new StockPriceConsumer();
			stockPriceConsumer.Subscribe(stockPriceUpdator);


			using ( var cts = new CancellationTokenSource( TimeSpan.FromSeconds( 30 ) ) )
			{
				await stockPriceUpdator.StartStockPriceUpdation( cts.Token );
			}

			Console.ReadKey();
		}

		
	}
}
