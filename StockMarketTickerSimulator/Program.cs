using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataGenerationEngine;
using StockTicker.Eventing;
using ClientSubscriptionModule;
namespace StockMarketTickerSimulator
{
	internal class Program
	{
		static async Task Main( string[] args )
		{
			UniqueStockSymbols uniqueStockSymbols = new UniqueStockSymbols();
			uniqueStockSymbols.InitializeUniqueStockSymbols();

			ClientStockConsumer clientStockConsumer = new ClientStockConsumer();
			clientStockConsumer.InitializeStockClientMapping();
			clientStockConsumer.InitializeClientStockMapping();

			StockPriceUpdator stockPriceUpdator = new StockPriceUpdator();
			clientStockConsumer.InitializeClientTaskForEachStock( stockPriceUpdator);

			
			using ( var cts = new CancellationTokenSource( TimeSpan.FromSeconds( 600 ) ) )
			{
				await stockPriceUpdator.StartStockPriceUpdation( cts.Token );
			}

			Console.ReadKey();
		}

		
	}
}
