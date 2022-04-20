using AF_RobotService.Models.Interfaces;
using Binance.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AF_RobotService.Models.Sides
{
    public class SideExit : ISide
    {
        public OrderSide GetSide()
        {
            return OrderSide.Sell;
        }
    }
}
