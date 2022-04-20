using Binance.Net;
using Binance.Net.Objects.Spot;
using CryptoExchange.Net.Authentication;
using System;

namespace BinanceService.Cores
{
    public sealed class BinanceClientSingleton
    {
        private BinanceClientSingleton() { }

        private static BinanceClient _binanceClient;
        private const string apikey = "CkZ4TFml4QG9iV3F0pFPU1S5pyKGX7BkT8k1LbH1LpSYOBmF45QwvoZjlJF6ogKE";
        private const string seckey = "41bj8gQH9WuE4OGiYUrYHKezOdycPmyfUtXgpP2rWWnFziOQwTEZEqS6yyYYugiR";
        public static BinanceClient GetInstance(string apiKey, string secretKey)
        {
            if (_binanceClient == null)
            {
                _binanceClient = new BinanceClient(new BinanceClientOptions()
                {
                    ApiCredentials = new ApiCredentials(apikey, seckey)
                });
            }
            return _binanceClient;
        }
        public static BinanceClient GetInstance()
        {
            if (_binanceClient == null)
            {
                return new BinanceClient(new BinanceClientOptions()
                {
                    ApiCredentials = new ApiCredentials(apikey, seckey)
                });
            }
            return _binanceClient;
        }
    }
}
