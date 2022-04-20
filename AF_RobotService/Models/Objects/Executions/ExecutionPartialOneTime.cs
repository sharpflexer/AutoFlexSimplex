using AF_RobotService.Models.Interfaces;

namespace AF_RobotService.Models.Executions
{
    public class ExecutionPartialOneTime : IExecution
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
        public ExecutionPartialOneTime(decimal quantity)
        {
            Quantity = quantity;
            TimesToExecute = 1;
        }

        public bool GetFullExecution()
        {
            return false;
        }
    }
}
