using Binance.Net;
using Binance.Net.Enums;
using BinanceService.Cores;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BinanceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {

        /* ФЬЮЧЕРСЫ
         * 
         * */

        [HttpPost("placeMarketOrderFut")]
        public string PlaceMarketOrderFut(string symbol, OrderSide orderSide, PositionSide positionSide, decimal percent)
        {
            var client = BinanceClientSingleton.GetInstance();

            decimal quantity;
            if (orderSide.Equals(OrderSide.Buy))
                quantity = GetAmountOutPosition(percent);
            else 
                quantity = GetAmountInPosition(percent);

            var result = client.FuturesUsdt.Order.PlaceOrder(
                symbol,
                orderSide,
                OrderType.Market,
                quantity,
                positionSide
                );
            if (result.Success)
                return result.Success.ToString();
            else
                return result.Error.Message.ToString();

        }


        // Ставит ордер по лимиту
        [HttpPost("placeLimitOrderFut")]
        public string PlaceLimitOrderFut(string symbol, OrderSide side, decimal quan, decimal pric, PositionSide pside)
        {
            var client = BinanceClientSingleton.GetInstance();
            var result = client.FuturesUsdt.Order.PlaceOrder(
                symbol,
                side,
                OrderType.Limit,
                (decimal?)quan,
                positionSide: pside,
                price: (decimal?)pric,
                timeInForce: TimeInForce.GoodTillCancel
                );
            if (result.Success)
                return result.Success.ToString();
            else
                return result.Error.ToString();
        }

        // Отменяет ордер
        [HttpPost("cancelOrderFut")]
        public string CancelOrderFut(string symbol, long orderID)
        {
            var client = BinanceClientSingleton.GetInstance();
            var result = client.FuturesUsdt.Order.CancelOrder(symbol, orderID);
            return result.ResponseStatusCode.ToString();
        }

        // Отменяет все ордера
        [HttpPost("cancelAllOrdersFut")]
        public string CancelAllOrdersFut(string symbol)
        {
            var client = BinanceClientSingleton.GetInstance();
            var result = client.FuturesUsdt.Order.CancelAllOrders(symbol);
            return result.ResponseStatusCode.ToString();
        }

        /* СПОТ
         * 
         * */
        [HttpPost("placeMarketOrder")]
        public string PlaceMarketOrder(string symbol, OrderSide side, decimal quantity)
        {
            var client = BinanceClientSingleton.GetInstance();
            var result = client.Spot.Order.PlaceOrder(
                symbol,
                side,
                OrderType.Market,
                quantity
                );
            if (result.Success)
                return result.Success.ToString();
            else
                return result.Error.Message.ToString();
        }


        // Ставит ордер по лимиту
        [HttpPost("placeLimitOrder")]
        public string PlaceLimitOrder(string symbol, OrderSide side, decimal quan, decimal pric)
        {
            var client = BinanceClientSingleton.GetInstance();
            var result = client.Spot.Order.PlaceOrder(
                symbol,
                side,
                OrderType.Limit,
                (decimal?)quan,
                price: (decimal?)pric,
                timeInForce: TimeInForce.GoodTillCancel
                );
            if (result.Success)
                return result.Success.ToString();
            else
                return result.Error.ToString();
        }

        // Отменяет ордер
        [HttpPost("cancelOrder")]
        public string CancelOrder(string symbol, long orderID)
        {
            var client = BinanceClientSingleton.GetInstance();
            var result = client.Spot.Order.CancelOrder(symbol, orderID);
            return result.ResponseStatusCode.ToString();
        }

        // Отменяет все ордера
        [HttpPost("cancelAllOrders")]
        public string CancelAllOrders(string symbol)
        {
            var client = BinanceClientSingleton.GetInstance();
            var result = client.Spot.Order.CancelAllOpenOrders(symbol);
            return result.ResponseStatusCode.ToString();
        }
        private decimal GetAmountOutPosition(decimal percent)
        {
            var client = BinanceClientSingleton.GetInstance();

            // По идее здесь можно обращаться по запросам(в CandlesController и AccountController)
            decimal BalanceUsdt = client.FuturesUsdt.Account.GetAccountInfo().Data.AvailableBalance;

            decimal result = Math.Round((percent / 100) * BalanceUsdt, 2);
            return result;
        }
        private decimal GetAmountInPosition(decimal percent)
        {
            var client = BinanceClientSingleton.GetInstance();
            // По идее здесь можно обращаться по запросам(в CandlesController и AccountController)
            decimal BalanceUsdt = client.FuturesUsdt.Account.GetAccountInfo().Data.TotalPositionInitialMargin;
            decimal result = Math.Round((percent / 100) * BalanceUsdt, 2);
            return result;
        }
    }



}
