using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerationEngine
{
	public class UniqueStockSymbols
	{
		public static ConcurrentDictionary<string, double> stockSymbols = new ConcurrentDictionary<string, double>();
		Random randomTicker = new Random();


		public void GenerateUniqueStockSymbols()
		{
			Console.WriteLine("Generating UniqueStock Symbols");
			while ( stockSymbols.Count < 100 )
			{
				StringBuilder stockSymbol = new StringBuilder();
				int stockSymbolLength = randomTicker.Next( 3, 5 );
				int stockSymbolValue = randomTicker.Next( 100, 5001 );

				for ( int i = 1; i <= stockSymbolLength; i++ )
				{
					char letter = ( char )randomTicker.Next( 'A', 'Z' + 1 );
					stockSymbol.Append( letter );
				}
				stockSymbols.TryAdd( stockSymbol.ToString(), stockSymbolValue );
			}

			foreach (double stockValue in stockSymbols.Values)
			{ 
				Console.WriteLine($"initial generated stockValue = {stockValue}");
			}	

			Console.WriteLine("Generated UniqueStock Symbols");
		}

	}
}
