using Binance.Net.Enums;

namespace AF_RobotService.Models.Interfaces
{
    public interface IPosition
    {
        public PositionSide GetPosition();
        public bool IsPositionShort();
    }
}