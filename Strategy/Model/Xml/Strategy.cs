using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace StrategyService.Model.Xml
{
    public class Strategy
    {
        [XmlAttribute("name")]
        public string NameStrategy { get; set; }

        [XmlElement("TradeType")]
        public List<TradeType> TradeType { get; set; }

        [XmlIgnore]
        public IEnumerable<InputIndicatorBox> InputIndicatorBoxes { get; set; }
        [XmlIgnore]
        public Dictionary<string, decimal> Constants { get; set; }
        public Strategy()
        {
        }
    

        public StrategyResult Execute(List<OutputIndicatorBox> outputIndicators)
        {
            foreach(TradeType tradeType in TradeType)
            {
                if (tradeType.Execute(outputIndicators, Constants))
                {
                    Signal signal = (Signal)Enum.Parse(typeof(Signal), tradeType.Name);
                    decimal quantity = tradeType.Quantity;
                    int id = tradeType.ID;
                    StrategyResult strategyResult = new StrategyResult(signal, quantity, id);
                    return strategyResult;
                }
            }
            return new StrategyResult(Signal.Nothing, 0, -1);
        }
    }
}
