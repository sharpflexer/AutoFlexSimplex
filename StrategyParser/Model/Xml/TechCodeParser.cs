using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyParser.Model.Xml
{
    public class TechCodeParser
    {
        public static readonly Dictionary<string, Func<decimal, decimal, bool>> operators = new Dictionary<string, Func<decimal, decimal, bool>>
        {
            { "above", (leftOperand, rightOperand) => leftOperand > rightOperand },
            { "above or equal", (leftOperand, rightOperand) => leftOperand >= rightOperand },
            { "under", (leftOperand, rightOperand) => leftOperand < rightOperand },
            { "under or equal", (leftOperand, rightOperand) => leftOperand <= rightOperand },
            { "equal", (leftOperand, rightOperand) => leftOperand == rightOperand },
            { "not equal", (leftOperand, rightOperand) => leftOperand != rightOperand },
        };
       
    }
}
