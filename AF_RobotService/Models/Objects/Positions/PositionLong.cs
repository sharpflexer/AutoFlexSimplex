using AF_RobotService.Models.Interfaces;
using Binance.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AF_RobotService.Models.Positions
{
    public class PositionLong : IPosition
    {
        public PositionSide GetPosition()
        {
            return PositionSide.Long;
        }

        public bool IsPositionShort()
        {
            return false;
        }
    }
}
