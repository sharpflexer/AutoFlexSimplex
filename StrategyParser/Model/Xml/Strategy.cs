using StrategyParser.Domain.Model.Xml.Indicator;
using StrategyParser.Model.Yml;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace StrategyParser.Model.Xml
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
        public Dictionary<string,decimal> Constants { get; set; }
        public Strategy()
        {
        }
    }
}
