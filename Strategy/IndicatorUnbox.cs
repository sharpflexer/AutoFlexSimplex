using StrategyService.Model;
using StrategyService.Model.Xml;
using System.Collections.Generic;

namespace StrategyService
{
    public class IndicatorUnbox
    {
        public List<InputIndicatorBox> indicatorsToBoxes(Strategy strategy)
        {
            List<InputIndicatorBox> indicatorBoxes = new List<InputIndicatorBox>();
            foreach (KeyValuePair<string, ParametresYml> keyValue in strategy.YmlContiner.Paramets)
            {
                InputIndicatorBox inputIndicator = new InputIndicatorBox();
                inputIndicator.codename = keyValue.Key;
                inputIndicator.name = keyValue.Value.Indicator;
                inputIndicator.candleNumber = keyValue.Value.CandleNumber;
                inputIndicator.prm = new List<decimal>();
                inputIndicator.prm.Add(keyValue.Value.Period);
                indicatorBoxes.Add(inputIndicator);
            }
            return indicatorBoxes;
        }
    }
}
