using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace StockTicker.Eventing
{
	public class PriceChangedEventArgs : EventArgs
	{
		public string Symbol { get; set; }
		public double OldPrice { get; set; }
		public double NewPrice { get; set; }
		public DateTime TimeStamp { get; set; }
		
		public override string ToString()
		{
			return $"Symbol = {Symbol}\n OldPrice = {OldPrice}\n NewPrice = {NewPrice}\n TimeStamp = {TimeStamp}\n\n";
		}

	}

}
