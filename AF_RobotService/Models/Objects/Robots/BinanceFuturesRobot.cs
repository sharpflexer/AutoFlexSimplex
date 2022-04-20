using AF_RobotService.Factories;
using AF_RobotService.Models.Interfaces;
using AF_RobotService.Models.Orders;
using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AF_RobotService.Models
{
    //strategy url: https://localhost:44373/api/strategy/GetStrategyResult
    public class BinanceFuturesRobot
    {
        const string apiKey = "CkZ4TFml4QG9iV3F0pFPU1S5pyKGX7BkT8k1LbH1LpSYOBmF45QwvoZjlJF6ogKE";
        const string secKey = "41bj8gQH9WuE4OGiYUrYHKezOdycPmyfUtXgpP2rWWnFziOQwTEZEqS6yyYYugiR";

        const string authenficationURI = "https://localhost:44319/api/authenfication/SetAPI?";
        const string strategyURI = "https://localhost:44373/api/strategy/GetStrategyResult";

        const string symbol = "BTCUSDT";

        public bool isWork;
        private IStatus robotStatus;
        Limits limits;

        public void Work()
        {
            Request.Send(authenficationURI, "POST", apiKey + "&" + secKey);
            limits = new Limits(Decimal.MaxValue, Decimal.MinValue, Decimal.MinValue, Decimal.MaxValue);
            while (isWork)
            {
                //StopLoss and TakeProfit
                LimitsConditions conditions = limits.CheckLimits(symbol);
                if (!robotStatus.IsInShortPosition)
                {
                    if (conditions.LongTakeCondition)
                    {
                        FuturesOrder order = OrderFactory.GetTakeProfitLong();
                        order.MakeOrder(symbol);
                    }
                    if (conditions.LongStopCondition)
                    {
                        FuturesOrder order = OrderFactory.GetStopLossLongOrder();
                        order.MakeOrder(symbol);
                        limits = order.Limits;
                    }
                }
                if (robotStatus.IsInShortPosition)
                {
                    if (conditions.ShortTakeCondition)
                    {
                        FuturesOrder order = OrderFactory.GetTakeProfitShort();
                        order.MakeOrder(symbol);
                    }
                    if (conditions.ShortStopCondition)
                    {
                        FuturesOrder order = OrderFactory.GetStopLossShortOrder();
                        order.MakeOrder(symbol);
                        limits = order.Limits;
                    }
                }
                string strategyResultString = Request.Send(strategyURI, "GET");
                StrategyResult strategyResult = JsonConvert.DeserializeObject<StrategyResult>(strategyResultString);

                if (SignalFilter.Check(strategyResult.StrategySignal, robotStatus))
                {
                    Dictionary<Signal, FuturesOrder> signalOrderConnections = new Dictionary<Signal, FuturesOrder>()
                    {
                        { Signal.EnterLong, OrderFactory.GetEnterLongOrder() },
                        { Signal.EnterShort, OrderFactory.GetEnterShortOrder() },
                        { Signal.ExitLong, OrderFactory.GetFullExitLongOrder() },
                        { Signal.ExitShort, OrderFactory.GetFullExitShortOrder() },
                    };
                    Dictionary<Signal, FuturesOrder> signalParialOrderConnections = new Dictionary<Signal, FuturesOrder>()
                    {
                        { Signal.ExitLong, OrderFactory.GetPartialExitLongOrder(strategyResult.Quantity) },
                        { Signal.ExitShort, OrderFactory.GetPartialExitShortOrder(strategyResult.Quantity) },
                    };
                    if (strategyResult.Quantity.Equals(100))
                    {
                        FuturesOrder order = signalOrderConnections.GetValueOrDefault(strategyResult.StrategySignal);
                        order.MakeOrder(symbol);
                        limits = order.Limits;
                    }
                    else if (strategyResult.Quantity > 0 && strategyResult.Quantity < 100)
                    {
                        FuturesOrder order = signalParialOrderConnections.GetValueOrDefault(strategyResult.StrategySignal);
                        order.MakeOrder(symbol);
                    }
                    else throw new Exception("Некорректное значение объёма сделки");
                }
            }
        }
    }
}
