using Binance.Net.Enums;

namespace AF_RobotService.Models.Interfaces
{
    public interface ISide
    {
        public OrderSide GetSide();
    }
}