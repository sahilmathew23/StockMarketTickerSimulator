using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StockTicker.Eventing;
namespace DataGenerationEngine
{
	public class StockPriceUpdator
	{
		private static readonly ThreadLocal<Random> threadLocalRandom = new ThreadLocal<Random>( () => new Random() );
		
		public event EventHandler<PriceChangedEventArgs> PriceChanged;
		public ConcurrentQueue<string> UpdatedStockSymbolNamesQueue = new ConcurrentQueue<string>();

		public async Task StartStockPriceUpdation( CancellationToken cancellationToken )
		{
			Console.WriteLine( "Updating Stock Values" );
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

							
							if ( UniqueStockSymbols.stockSymbols.TryUpdate( key, newValue, currentValue ) )
							{

								UpdatedStockSymbolNamesQueue.Append(key);
								PriceChanged?.Invoke( this, new PriceChangedEventArgs { OldPrice = currentValue, NewPrice = newValue, Symbol = key, TimeStamp = DateTime.Now } );

							}
						}

					}
						, cancellationToken
					);


					Console.WriteLine( $"Updated Stock Values for iteration = {count}\n" );

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
