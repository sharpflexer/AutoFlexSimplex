using Binance.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trady.Core.Infrastructure;

namespace BinanceService.Cores
{
    public class BinanceCandle : IOhlcv
    {
        public decimal Open { get ; set ; }
        public decimal High { get ; set ; }
        public decimal Low { get ; set ; }
        public decimal Close { get ; set ; }
        public decimal Volume { get ; set ; }

        public DateTimeOffset DateTime { get; set; }

        public static IEnumerable<BinanceCandle> ConvertToCandle(IEnumerable<IBinanceKline> candles)
        {
            List <BinanceCandle> list = new List<BinanceCandle>();
            try
            {
                foreach (IBinanceKline kline in candles)
                {
                    list.Add(new BinanceCandle() { DateTime = kline.OpenTime, Open = kline.Open, High = kline.High, Low = kline.Low, Close = kline.Close, Volume = kline.BaseVolume });
                }
            }
            catch(ArgumentNullException)
            {
                Thread.Sleep(5000);
                return ConvertToCandle(candles);
            }
            return list;
        }
    }
}
