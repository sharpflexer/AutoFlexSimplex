namespace AF_RobotService.Models.Interfaces
{
    public interface IStatus
    {
        public bool WasEnterInPosition { get; set;}
        public bool IsInShortPosition { get; set; }
    }
}