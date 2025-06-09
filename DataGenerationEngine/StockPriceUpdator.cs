using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace DataGenerationEngine
{
	public class StockPriceUpdator
	{
		private static readonly ThreadLocal<Random> threadLocalRandom = new ThreadLocal<Random>( () => new Random() );

		public async Task StartStockPriceUpdation( CancellationToken cancellationToken)
		{
			try 
			{
				int count = 1;
				while ( !cancellationToken.IsCancellationRequested )
				{
					
					var rndPriceChange = threadLocalRandom.Value;
					await Task.Run
					( () =>
					{
						foreach ( string key in UniqueStockSymbols.stockSymbols.Keys.ToList() )
						{
							double currentValue = UniqueStockSymbols.stockSymbols[ key ];
							double newValue = currentValue + currentValue * (rndPriceChange.Next( -5, 6 ) / 100.0);
							UniqueStockSymbols.stockSymbols.TryUpdate( key, newValue, currentValue );
						}

					}
						, cancellationToken
					);

					foreach ( double stockValue in UniqueStockSymbols.stockSymbols.Values )
					{
						Console.WriteLine( $"iteration {count}, updated stockValue = {stockValue}" );

					}

					await Task.Delay( TimeSpan.FromSeconds( 10 ), cancellationToken );
					count++;
				}

			}
			catch(OperationCanceledException)
			{
				Console.WriteLine("Stock price updation was cancelled. ");
			}
		}
		
	}
}
