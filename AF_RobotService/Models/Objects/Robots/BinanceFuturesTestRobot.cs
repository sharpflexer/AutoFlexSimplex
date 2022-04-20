using AF_RobotService.Factories;
using AF_RobotService.Models.Interfaces;
using AF_RobotService.Models.Objects.Orders;
using AF_RobotService.Models.Objects.Statuses;
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

namespace AF_RobotService.Models.Objects.Robots
{
    public class BinanceFuturesTestRobot
    {
        const string testFilePath = "D:\\AutoFlex\\TestFiles\\LogsV1.txt";

        const string apiKey = "CkZ4TFml4QG9iV3F0pFPU1S5pyKGX7BkT8k1LbH1LpSYOBmF45QwvoZjlJF6ogKE";
        const string secKey = "41bj8gQH9WuE4OGiYUrYHKezOdycPmyfUtXgpP2rWWnFziOQwTEZEqS6yyYYugiR";

        const string authenficationURI = "https://localhost:44319/api/authenfication/SetAPI?";
        const string strategyURI = "https://localhost:44373/api/strategy/GetStrategyResult";

        const string symbol = "BTCUSDT";

        public bool isWork;
        private IStatus robotStatus = new StatusEnter();
        Limits limits;
        private int nothingCount = 0;
        private DateTime timeStamp;
        TestOrder lastOrder;
        int executedTimes = 0;

        public void Work()
        {
            timeStamp = DateTime.Now;
            Request.Send(authenficationURI, "POST", apiKey + "&" + secKey);
            limits = new Limits(Decimal.MinValue, Decimal.MaxValue, Decimal.MaxValue, Decimal.MinValue);
            while (isWork)
            {
                //StopLoss and TakeProfit
                LimitsConditions conditions = limits.CheckLimits(symbol);
                if (!robotStatus.IsInShortPosition)
                {
                    if (conditions.LongTakeCondition)
                    {
                        
                        TestOrder order = TestOrderFactory.GetTakeProfitLong();
                        if (!order.Equals(lastOrder) || executedTimes < order.Execution.TimesToExecute)
                        {
                            lastOrder = order;
                            order.MakeOrder(symbol);
                        }
                    }
                    if (conditions.LongStopCondition)
                    {
                        TestOrder order = TestOrderFactory.GetStopLossLongOrder();
                        order.MakeOrder(symbol);
                        robotStatus = order.Status;
                        limits = order.Limits;
                    }
                }
                if (robotStatus.IsInShortPosition)
                {
                    if (conditions.ShortTakeCondition)
                    {
                        TestOrder order = TestOrderFactory.GetTakeProfitShort();
                        if (!order.Equals(lastOrder) || executedTimes < order.Execution.TimesToExecute)
                        {
                            lastOrder = order;
                            order.MakeOrder(symbol);
                        }
                    }
                    if (conditions.ShortStopCondition)
                    {
                        TestOrder order = TestOrderFactory.GetStopLossShortOrder();
                        order.MakeOrder(symbol);
                        robotStatus = order.Status;
                        limits = order.Limits;
                    }
                }
                string strategyResultString = Request.Send(strategyURI, "GET");
                StrategyResult strategyResult = JsonConvert.DeserializeObject<StrategyResult>(strategyResultString);

                if (SignalFilter.Check(strategyResult.StrategySignal, robotStatus))
                {
                    Dictionary<Signal, TestOrder> signalOrderConnections = new Dictionary<Signal, TestOrder>()
                    {
                        { Signal.EnterLong, TestOrderFactory.GetEnterLongOrder() },
                        { Signal.EnterShort, TestOrderFactory.GetEnterShortOrder() },
                        { Signal.ExitLong, TestOrderFactory.GetFullExitLongOrder() },
                        { Signal.ExitShort, TestOrderFactory.GetFullExitShortOrder() },
                    };
                    Dictionary<Signal, TestOrder> signalParialOrderConnections = new Dictionary<Signal, TestOrder>()
                    {
                        { Signal.ExitLong, TestOrderFactory.GetPartialExitLongOrder(strategyResult.Quantity) },
                        { Signal.ExitShort, TestOrderFactory.GetPartialExitShortOrder(strategyResult.Quantity) },
                    };
                    if (strategyResult.Quantity.Equals(100))
                    {
                        TestOrder order = signalOrderConnections.GetValueOrDefault(strategyResult.StrategySignal);
                        order.MakeOrder(symbol);
                        robotStatus = order.Status;
                        limits = order.Limits;
                    }
                    else if (strategyResult.Quantity > 0 && strategyResult.Quantity < 100)
                    {
                        TestOrder order = signalParialOrderConnections.GetValueOrDefault(strategyResult.StrategySignal);
                        if (!order.Equals(lastOrder) || executedTimes < order.Execution.TimesToExecute)
                        {
                            lastOrder = order;
                            order.MakeOrder(symbol);
                        }
                    }
                    else throw new Exception("Некорректное значение объёма сделки");
                }
                else
                {
                    nothingCount++;
                    DateTime currentTime = DateTime.Now;
                    double diff = currentTime.Subtract(timeStamp).TotalSeconds;
                    if(diff >= 300)
                    {
                        WriteLogs();
                        timeStamp = currentTime;
                        nothingCount = 0;
                    }
                }
            }
        }

        private void WriteLogs()
        {
            StringBuilder data = new StringBuilder();
            data.Append("***Regular logging message***\n");
            data.Append("Count of \"Nothing\" by 5 minutes = " + nothingCount + "\n");

            StreamWriter sw = new StreamWriter(testFilePath, true);
            sw.WriteLine(data.ToString());
            sw.Close();
        }
    }
    
}
