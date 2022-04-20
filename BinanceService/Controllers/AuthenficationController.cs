using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.SpotData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CryptoExchange.Net.Authentication;
using Binance.Net.Objects.Spot;
using BinanceService.Cores;

namespace BinanceService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenficationController : ControllerBase
    {
        // Аутенфикация
        [HttpPost]
        public void SetAPI(string apiKey, string secretKey)
        {
            BinanceClientSingleton.GetInstance(apiKey, secretKey);
        }
    }
}
