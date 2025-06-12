using DataGenerationEngine;
using StockTicker.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Logger;
namespace ClientSubscriptionModule
{
	public class Client
	{
		public string ClientName { get; set; }
		DateTime oldDateTime { get; set; }
		public void OnPriceChangedForClients(object sender , PriceChangedEventArgs e)
		{
			Task.Run( () => 
			{
				
				if ( DateTime.MinValue!= oldDateTime && (e.TimeStamp-oldDateTime).TotalSeconds>5)
				{ 
					StockPriceUpdator stockPriceUpdator = sender as StockPriceUpdator;
					stockPriceUpdator.PriceChanged-=OnPriceChangedForClients;
					Log.WriteLog( $"[Client: {ClientName}] has unsubscribed Stock {e.Symbol} as last notification received = {oldDateTime} and current time = {e.TimeStamp} " );
				}
				oldDateTime = e.TimeStamp;
				Log.WriteLog( $"[Client: {ClientName}] Stock {e.Symbol} changed from {e.OldPrice} to {e.NewPrice} at {e.TimeStamp}" );
				
			} );
		}
	}
}
