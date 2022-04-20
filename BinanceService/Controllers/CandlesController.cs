using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Spot.MarketData;
using BinanceService.Controllers;
using BinanceService.Cores;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace BinanceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandlesController : ControllerBase
    {

        // Свечи
        [HttpGet("CandlesDataFut")]
        public IEnumerable<IBinanceKline> GetDataFut(string pair, int interval)
        {
            var client = BinanceClientSingleton.GetInstance();
            var klines = client.FuturesUsdt.Market.GetKlines(pair, (KlineInterval)interval);
            return klines.Data;
        }

        // тек.цена фьюч
        [HttpGet("PriceFut")]
        public BinancePrice GetPriceFut(string symbol)
        {
            var client = BinanceClientSingleton.GetInstance();
            var result = client.FuturesUsdt.Market.GetPrice(symbol).Data;
            return result;
        }

        // ФЬЮЧЕРСЫ

        [HttpGet("CandlesData")]
        public IEnumerable<BinanceCandle> GetData(string pair, int interval)
        {
            var client = BinanceClientSingleton.GetInstance();
            var klines = client.Spot.Market.GetKlines(pair, (KlineInterval)interval);
            return BinanceCandle.ConvertToCandle(klines.Data);
        }

        // тек.цена спот
        [HttpGet("Price")]
        public decimal GetPrice(string symbol)
        {
            var client = BinanceClientSingleton.GetInstance();
            var result = client.Spot.Market.GetPrice(symbol).Data;
            return result.Price;
        }

        

    }
}
