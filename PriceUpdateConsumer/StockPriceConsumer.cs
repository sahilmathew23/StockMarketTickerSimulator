using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGenerationEngine;
using StockTicker.Eventing;
using Logger;
using System.Reflection;
namespace PriceUpdateConsumer
{
    public class StockPriceConsumer
    {
        public void Subscribe(StockPriceUpdator stockPriceUpdator)
        {
            stockPriceUpdator.PriceChanged += GetNotification;
        }

        public void GetNotification(object sender, PriceChangedEventArgs priceChangedEventArgs)
        {
            Log.WriteLog( $"{MethodBase.GetCurrentMethod()} StockPriceChanged!!!\n {priceChangedEventArgs}" );
        }
    }
}
