
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyService.Model
{
    [Serializable]
    public class YmlContiner
    {
       public Dictionary <string, ParametresYml> Paramets { get; set; }
    }
}
