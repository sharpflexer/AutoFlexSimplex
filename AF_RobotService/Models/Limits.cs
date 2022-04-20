using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AF_RobotService.Models
{
    public class Limits
    {
        const string priceURI = "https://localhost:44319/api/candles/Price?";
        public decimal LongTake { get; private set; }
        public decimal LongStop { get; private set; }
        public decimal ShortTake { get; private set; }
        public decimal ShortStop { get; private set; }

        public Limits(decimal longTake, decimal longStop, decimal shortTake, decimal shortStop)
        {
            LongTake = longTake;
            LongStop = longStop;
            ShortTake = shortTake;
            ShortStop = shortStop;
        }

        public LimitsConditions CheckLimits(string symbol)
        {
            string hueta = Request.Send(priceURI, "GET", "symbol=" + symbol).Replace('.', ',');
            decimal price = Convert.ToDecimal(hueta);
            bool longTakeCondition = LongTake <= price;
            bool longStopCondition = LongStop >= price;
            bool shortTakeCondition = ShortTake >= price;
            bool shortStopCondition = ShortStop <= price;
            LimitsConditions limitsConditions = new LimitsConditions(longTakeCondition, longStopCondition, shortTakeCondition, shortStopCondition);
            return limitsConditions;
        }
    }
}
