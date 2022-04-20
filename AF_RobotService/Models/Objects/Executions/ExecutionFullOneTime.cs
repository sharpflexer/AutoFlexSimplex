using AF_RobotService.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AF_RobotService.Models.Executions
{
    public class ExecutionFullOneTime : IExecution
    {
        private decimal quantity;
        public decimal Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }

        private int timesToExecute;
        public int TimesToExecute
        {
            get
            {
                return timesToExecute;
            }
            set
            {
                timesToExecute = value;
            }
        }
        public ExecutionFullOneTime()
        {
            Quantity = 100;
            TimesToExecute = 1;
        }

        public bool GetFullExecution()
        {
            return true;
        }
    }
}
