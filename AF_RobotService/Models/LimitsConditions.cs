using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AF_RobotService.Models
{
    public class LimitsConditions
    {
        public bool LongTakeCondition { get; private set; }
        public bool LongStopCondition { get; private set; }
        public bool ShortTakeCondition { get; private set; }
        public bool ShortStopCondition { get; private set; }

        public LimitsConditions(bool longTakeCondition, bool longStopCondition, bool shortTakeCondition, bool shortStopCondition)
        {
            LongTakeCondition = longTakeCondition;
            LongStopCondition = longStopCondition;
            ShortTakeCondition = shortTakeCondition;
            ShortStopCondition = shortStopCondition;
        }
    }
}
