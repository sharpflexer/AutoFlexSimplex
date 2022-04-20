using Binance.Net.Objects.Futures.FuturesData;
using Binance.Net.Objects.Futures.MarketData;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.SpotData;
using BinanceService.Cores;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BinanceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        // Баланс и информация об аккаунте

        // Futures
        [HttpGet("BalancesFut")]
        public BinanceFuturesAccountInfo GetBalancesFut()
        {
            var client = BinanceClientSingleton.GetInstance();
            var result = client.FuturesUsdt.Account.GetAccountInfo();
            return result.Data;
        }

        [HttpGet("ListSymbolsFut")]
        public IEnumerable<BinanceFuturesUsdtSymbol> GetListSymbolsFut()
        {
            var client = BinanceClientSingleton.GetInstance();
            var result = client.FuturesUsdt.System.GetExchangeInfo().Data.Symbols;
            return result;
        }

        // SPOT
        [HttpGet("Balances")]
        public BinanceAccountInfo GetBalances()
        {
            var client = BinanceClientSingleton.GetInstance();
            var result = client.General.GetAccountInfo();
            return result.Data;
        }

        // Список торговых пар
        [HttpGet("ListSymbols")]
        public IEnumerable<BinanceSymbol> GetListSymbols()
        {
            var client = BinanceClientSingleton.GetInstance();
            var result = client.Spot.System.GetExchangeInfo().Data.Symbols;
            return result;
        }



    }
}
