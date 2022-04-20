using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AF_RobotService.Models
{
    public class StrategyResult
    {
        public Signal StrategySignal { get; private set; }
        public decimal Quantity { get; private set; }

        public StrategyResult(Signal signal, decimal quantity)
        {
            StrategySignal = signal;
            Quantity = quantity;
        }
    }
}

