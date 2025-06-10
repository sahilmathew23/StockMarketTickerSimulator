using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
	public static class Log
	{
		private static readonly string logFilePath = $@"C:\Users\sahil\source\repos\StockMarketTickerSimulator\logs\file_{DateTime.Now.ToFileTimeUtc() + ".txt"}";
		private static readonly object lockObj = new object();
		public static void WriteLog( string log )
		{
			lock ( lockObj )
			{
				using ( StreamWriter writer = new StreamWriter( logFilePath, true ) )
				{
					Console.WriteLine( log );
					writer.WriteLine( log );
				}
			}
		}
	}
}
