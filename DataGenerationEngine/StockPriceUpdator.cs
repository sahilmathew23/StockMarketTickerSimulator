using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StockTicker.Eventing;
using Logger;
namespace DataGenerationEngine
{
	public class StockPriceUpdator
	{
		private static readonly ThreadLocal<Random> threadLocalRandom = new ThreadLocal<Random>( () => new Random() );
		
		public event EventHandler<PriceChangedEventArgs> PriceChanged;

		public async Task StartStockPriceUpdation( CancellationToken cancellationToken )
		{
			Console.WriteLine( "Updating Stock Values" );
			try 
			{
				int count = 1;
				while ( !cancellationToken.IsCancellationRequested )
				{
					
					var rndPriceChange = threadLocalRandom.Value;
					var rndStockUpdateTimeChange = threadLocalRandom.Value;
					await Task.Run
					( async () =>
					{
						foreach ( string key in UniqueStockSymbols.stockSymbols.Keys.ToList() )
						{
							double currentValue = UniqueStockSymbols.stockSymbols[ key ];
							double newValue = currentValue + currentValue * (rndPriceChange.Next( -5, 6 ) / 100.0);

							
							if ( UniqueStockSymbols.stockSymbols.TryUpdate( key, newValue, currentValue ) )
							{

								
								PriceChanged?.Invoke( this, new PriceChangedEventArgs { OldPrice = currentValue, NewPrice = newValue, Symbol = key, TimeStamp = DateTime.Now } );
								await Task.Delay( TimeSpan.FromSeconds( rndStockUpdateTimeChange.Next( 0, 7 ) ), cancellationToken );

							}
						}

					}
						, cancellationToken
					);


					Console.WriteLine( $"Updated Stock Values for iteration = {count}\n" );


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
