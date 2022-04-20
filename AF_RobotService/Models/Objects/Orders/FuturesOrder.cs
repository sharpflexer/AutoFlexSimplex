using AF_RobotService.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AF_RobotService.Models.Orders
{
    public class FuturesOrder
    {
        private const string binanceOrderUri = "https://localhost:44319/api/orders/placeFuturesMarketOrder?";
        public ISide Side { get; private set; }
        public IPosition Position { get; private set; }
        public IExecution Execution { get; private set; }
        public IStatus Status { get; private set; }
        public Limits Limits { get; private set; }

        public FuturesOrder(ISide side, IPosition position, IExecution execution, IStatus status, Limits limits)
        {
            Side = side;
            Position = position;
            Execution = execution;
            Status = status;
            Limits = limits;
        }

        public void MakeOrder(string symbol)
        {
            StringBuilder data = new StringBuilder();
            data.Append("symbol=" + symbol + "&");
            data.Append("orderSide=" + Side.GetSide() + "&");
            data.Append("positionSide=" + Position.GetPosition() + "&");
            data.Append("percent=" + Execution.Quantity);
            string jsonData = data.ToString(); 

            Request.Send(binanceOrderUri, "POST", jsonData);
        }
    }
}
