using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyService.Model
{
    [Serializable]
    public class ParametresYml
    {
        public string Indicator { get; set; }
        public int Period { get; set; }
        public int CandleNumber { get; set; }
    }
}
