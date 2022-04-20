using AF_RobotService.Models;
using AF_RobotService.Models.Executions;
using AF_RobotService.Models.Interfaces;
using AF_RobotService.Models.Objects.Orders;
using AF_RobotService.Models.Objects.Statuses;
using AF_RobotService.Models.Orders;
using AF_RobotService.Models.Positions;
using AF_RobotService.Models.Sides;
using System;
using System.Text;

namespace AF_RobotService.Factories
{
    public class TestOrderFactory
    {
        const string postitivePriceURI = "https://localhost:44371/api/indicator/GetPricePositiveLevel?";
        const string negativePriceURI = "https://localhost:44371/api/indicator/GetPriceNegativeLevel?";
        public static TestOrder GetEnterLongOrder()
        {
            ISide side = new SideEnter();
            IPosition position = new PositionLong();
            IExecution execution = new ExecutionFullOneTime();
            IStatus status = new StatusExitLong();
            Limits limits = GetEnterLongLimits();
            TestOrder order = new TestOrder(side, position, execution, status, limits);
            return order;
        }

        public static TestOrder GetEnterShortOrder()
        {
            ISide side = new SideEnter();
            IPosition position = new PositionShort();
            IExecution execution = new ExecutionFullOneTime();
            IStatus status = new StatusExitShort();
            Limits limits = GetEnterShortLimits();
            TestOrder order = new TestOrder(side, position, execution, status, limits);
            return order;
        }

        public static TestOrder GetFullExitLongOrder()
        {
            ISide side = new SideExit();
            IPosition position = new PositionLong();
            IExecution execution = new ExecutionFullOneTime();
            IStatus status = new StatusEnter();
            Limits limits = GetExitLongLimits();
            TestOrder order = new TestOrder(side, position, execution, status, limits);
            return order;
        }

        public static TestOrder GetPartialExitLongOrder(decimal quantity)
        {
            ISide side = new SideExit();
            IPosition position = new PositionLong();
            IExecution execution = new ExecutionPartialOneTime(quantity);
            IStatus status = new StatusEnter();
            TestOrder order = new TestOrder(side, position, execution, status, null);
            return order;
        }

        public static TestOrder GetFullExitShortOrder()
        {
            ISide side = new SideExit();
            IPosition position = new PositionShort();
            IExecution execution = new ExecutionFullOneTime();
            IStatus status = new StatusEnter();
            Limits limits = GetExitShortLimits();
            TestOrder order = new TestOrder(side, position, execution, status, limits);
            return order;
        }

        public static TestOrder GetPartialExitShortOrder(decimal quantity)
        {
            ISide side = new SideExit();
            IPosition position = new PositionShort();
            IExecution execution = new ExecutionPartialOneTime(quantity);
            IStatus status = new StatusEnter();
            TestOrder order = new TestOrder(side, position, execution, status, null);
            return order;
        }
        public static TestOrder GetStopLossLongOrder()
        {
            ISide side = new SideExit();
            IPosition position = new PositionLong();
            IExecution execution = new ExecutionFullOneTime();
            IStatus status = new StatusEnter();
            Limits limits = GetExitLongLimits();
            TestOrder order = new TestOrder(side, position, execution, status, limits);
            return order;
        }
        public static TestOrder GetStopLossShortOrder()
        {
            ISide side = new SideExit();
            IPosition position = new PositionShort();
            IExecution execution = new ExecutionFullOneTime();
            IStatus status = new StatusEnter();
            Limits limits = GetExitShortLimits();
            TestOrder order = new TestOrder(side, position, execution, status, limits);
            return order;
        }
        public static TestOrder GetTakeProfitLong()
        {
            ISide side = new SideExit();
            IPosition position = new PositionLong();
            IExecution execution = new ExecutionPartialOneTime(60);
            IStatus status = new StatusEnter();
            TestOrder order = new TestOrder(side, position, execution, status, null);
            return order;
        }

        public static TestOrder GetTakeProfitShort()
        {
            ISide side = new SideExit();
            IPosition position = new PositionShort();
            IExecution execution = new ExecutionPartialOneTime(60);
            IStatus status = new StatusEnter();
            TestOrder order = new TestOrder(side, position, execution, status, null);
            return order;
        }

        private static Limits GetEnterLongLimits()
        {
            decimal longTake = GetLongTake();
            decimal longStop = GetLongStop();
            decimal shortTake = decimal.MinValue;
            decimal shortStop = decimal.MaxValue;
            Limits limits = new Limits(longTake, longStop, shortTake, shortStop);
            return limits;
        }

        private static Limits GetEnterShortLimits()
        {
            decimal longTake = decimal.MaxValue;
            decimal longStop = decimal.MinValue;
            decimal shortTake = GetShortTake();
            decimal shortStop = GetShortStop();
            Limits limits = new Limits(longTake, longStop, shortTake, shortStop);
            return limits;
        }

        private static Limits GetExitLongLimits()
        {
            decimal longTake = decimal.MaxValue;
            decimal longStop = decimal.MinValue;
            decimal shortTake = decimal.MinValue;
            decimal shortStop = decimal.MaxValue;
            Limits limits = new Limits(longTake, longStop, shortTake, shortStop);
            return limits;
        }

        private static Limits GetExitShortLimits()
        {
            decimal longTake = decimal.MaxValue;
            decimal longStop = decimal.MinValue;
            decimal shortTake = decimal.MinValue;
            decimal shortStop = decimal.MaxValue;
            Limits limits = new Limits(longTake, longStop, shortTake, shortStop);
            return limits;
        }

        private static decimal GetLongTake()
        {
            return GetPriceLevel(postitivePriceURI, 4);
        }

        private static decimal GetLongStop()
        {
            return GetPriceLevel(negativePriceURI, 3);
        }

        private static decimal GetShortTake()
        {
            return GetPriceLevel(negativePriceURI, 4);
        }

        private static decimal GetShortStop()
        {
            return GetPriceLevel(postitivePriceURI, 3);
        }

        private static decimal GetPriceLevel(string path, int mult)
        {
            StringBuilder data = new StringBuilder();
            data.Append("mult=" + mult);
            string jsonData = data.ToString();

            decimal price = Convert.ToDecimal(Request.Send(path, "GET", jsonData));
            return price;
        }
    }
}
