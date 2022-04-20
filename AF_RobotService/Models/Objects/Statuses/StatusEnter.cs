using AF_RobotService.Models.Interfaces;


namespace AF_RobotService.Models.Objects.Statuses
{
    public class StatusEnter : IStatus
    {
        public bool WasEnterInPosition { get; set; }
        public bool IsInShortPosition { get; set; }

        public StatusEnter()
        {
            WasEnterInPosition = true;
            IsInShortPosition = false;
        }
    }
}
