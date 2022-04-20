namespace AF_RobotService.Models.Interfaces
{
    public interface IExecution
    {
        decimal Quantity 
        {
            get; 
            set; 
        }
        int TimesToExecute 
        { 
            get;
            set; 
        }

        bool GetFullExecution();
    }
}