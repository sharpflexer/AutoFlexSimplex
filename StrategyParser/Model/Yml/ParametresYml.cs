using StrategyParser.Domain.Model.Yml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyParser.Model.Yml
{
    [Serializable]
    public class ParametresYml
    {
        public Dictionary<string, IndicatorYml> Indicator{ get; set; }
        public Dictionary<string, decimal> Constant { get; set; } 
    }
}
