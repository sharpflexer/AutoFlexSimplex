using AF_RobotService.Models.Interfaces;



namespace AF_RobotService.Models.Objects.Statuses
{
    public class StatusExitLong : IStatus
    {
        public bool WasEnterInPosition { get; set; }
        public bool IsInShortPosition { get; set; }

        public StatusExitLong()
        {
            WasEnterInPosition = false;
            IsInShortPosition = false;
        }
    }
}
