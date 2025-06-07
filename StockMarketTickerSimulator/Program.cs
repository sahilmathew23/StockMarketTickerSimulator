using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGenerationEngine;
namespace StockMarketTickerSimulator
{
	internal class Program
	{
		static void Main( string[] args )
		{
			UniqueStockSymbols uniqueStockSymbols = new UniqueStockSymbols();
			uniqueStockSymbols.generateUniqueStockSymbols();
		}
	}
}
