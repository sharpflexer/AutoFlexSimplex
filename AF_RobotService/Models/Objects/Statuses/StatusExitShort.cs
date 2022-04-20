using AF_RobotService.Models.Interfaces;



namespace AF_RobotService.Models.Objects.Statuses
{
    public class StatusExitShort : IStatus
    {
        public bool WasEnterInPosition { get; set; }
        public bool IsInShortPosition { get; set; }

        public StatusExitShort()
        {
            WasEnterInPosition = false;
            IsInShortPosition = true;
        }
    }
}
