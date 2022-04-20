using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyService
{
    public class StrategyResult
    {
        public Signal StrategySignal {get; private set;}
        public decimal Quantity { get; private set; }

        public int ID { get; private set; }

        public StrategyResult(Signal signal, decimal quantity, int id)
        {
            StrategySignal = signal;
            Quantity = quantity;
            ID = id;
        }
    }
}
