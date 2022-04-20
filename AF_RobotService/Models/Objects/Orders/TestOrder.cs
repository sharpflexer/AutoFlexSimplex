using AF_RobotService.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AF_RobotService.Models.Objects.Orders
{
    public class TestOrder
    {
        private const string testFilePath = "D:\\AutoFlex\\TestFiles\\LogsV1.txt";
        public ISide Side { get; private set; }
        public IPosition Position { get; private set; }
        public IExecution Execution { get; private set; }
        public IStatus Status { get; private set; }
        public Limits Limits { get; private set; }

        public TestOrder(ISide side, IPosition position, IExecution execution, IStatus status, Limits limits)
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
            data.Append("New order was placed\n");
            data.Append("Symbol = " + symbol + "\n");
            data.Append("OrderSide =" + Side.GetSide() + "\n");
            data.Append("PositionSide = " + Position.GetPosition() + "\n");
            data.Append("Percent = " + Execution.Quantity + "\n");
            data.Append("Result Status: Is Enter? = " + Status.WasEnterInPosition);
                         data.Append(", Is Short? = " + Status.IsInShortPosition + "\n");
            data.Append("DateTime = " + DateTime.Now.ToString() + "\n");

            StreamWriter sw = new StreamWriter(testFilePath, true);
            sw.WriteLine(data.ToString());
            sw.Close();
        }
    }
}
