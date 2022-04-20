using System;
using System.Collections.Generic;

namespace StrategyService.Model.Xml
{
    public class Term
    {
        public string LeftOperand { get; set; }
        public string Operator { get; set; }
        public string RigthOperand { get; set; }

        public bool Execute(List<OutputIndicatorBox> outputIndicators, Dictionary<string, decimal> constants)
        {
            decimal leftVariable = FindVariable(outputIndicators, constants, LeftOperand);
            decimal rightVariable = FindVariable(outputIndicators, constants, RigthOperand);
            return TechCodeParser.operators[Operator](leftVariable, rightVariable);
        }

        private decimal FindVariable(List<OutputIndicatorBox> outputIndicators, Dictionary<string, decimal> constants, string variableName)
        {
            OutputIndicatorBox box = outputIndicators.Find(element => element.codename.Equals(variableName));
            if (box != null)
                return box.value;
            else if (constants.ContainsKey(variableName))
                return constants.GetValueOrDefault(variableName);
            else
                throw new Exception("Ошибка: неизвестная YML-переменная");
        }
        public Term()
        {

        }
    }
}
