using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StrategyParser.Model.Xml
{
    public class Term
    {
        public string LeftOperand { get; set; }         
        public string Operator { get; set; }   
        public string RigthOperand { get; set; }
       
        public bool Execute(Dictionary<string, decimal> calculatedIndicators)
        {
            return TechCodeParser.operators[Operator](calculatedIndicators[LeftOperand], calculatedIndicators[RigthOperand]);
        }
        public Term()
        {

        }       
    }
}
