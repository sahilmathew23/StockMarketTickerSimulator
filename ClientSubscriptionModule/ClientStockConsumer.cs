using ClientSubscriptionModule;
using DataGenerationEngine;
using StockTicker.Eventing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ClientSubscriptionModule
{
	public class ClientStockConsumer
	{
		public static ConcurrentDictionary<string, List<Client>> stockAndClientsMapping = new ConcurrentDictionary<string, List<Client>>();
		

		public List<Client> clients = new List<Client>
			{
				new Client { ClientName = "Alpha Investments" },    // 0–19
                new Client { ClientName = "Beta Capital" },          // 20–39
                new Client { ClientName = "Gamma Ventures" },        // 30–49
                new Client { ClientName = "Delta Traders" },         // 40–59
                new Client { ClientName = "Epsilon Partners" },      // 50–69
                new Client { ClientName = "Zeta Holdings" },         // 60–79
                new Client { ClientName = "Eta Securities" },        // 70–89
                new Client { ClientName = "Theta Corp" }             // 85–99
            };

		public async void InitializeTaskForEachClient(StockPriceUpdator stockPriceUpdator)
		{
			
			foreach ( var client in clients )
			{
				await Task.Run(
					() =>
					{
						stockPriceUpdator.PriceChanged += client.OnPriceChangedForClients;
						var sendingTimestamp = DateTime.Now;
					} );
			}

			
		}


		public void InitializeStockClientMapping()
		{
			var stockNames = new List<string>( UniqueStockSymbols.stockSymbols.Keys );

			for ( int i = 0; i < stockNames.Count; i++ )
			{
				var assignedClients = new List<Client>();

				if ( i >= 0 && i < 20 )
					assignedClients.Add( clients[ 0 ] );
				if ( i >= 20 && i < 40 )
					assignedClients.Add( clients[ 1 ] );
				if ( i >= 30 && i < 50 )
					assignedClients.Add( clients[ 2 ] );
				if ( i >= 40 && i < 60 )
					assignedClients.Add( clients[ 3 ] );
				if ( i >= 50 && i < 70 )
					assignedClients.Add( clients[ 4 ] );
				if ( i >= 60 && i < 80 )
					assignedClients.Add( clients[ 5 ] );
				if ( i >= 70 && i < 90 )
					assignedClients.Add( clients[ 6 ] );
				if ( i >= 85 && i < 100 )
					assignedClients.Add( clients[ 7 ] );

				stockAndClientsMapping.TryAdd( stockNames[ i ], assignedClients );
			}
		}

	}
}
