using StrategyParser.Domain.Model.Xml.LogicContainers;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace StrategyParser.Model.Xml
{
    [Serializable]
    public class TradeType
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("quantity")]
        public decimal Quantity { get; set; }
        [XmlAttribute("id")]
        public int ID { get; set; }
        [XmlElement("Сonjunction")]
        public List<Сonjunction> logicContainerConjunct;

        [XmlElement("Disjunction")]
        public List<Disjunction> logicContainerDisjunction;

        public TradeType() { }
    }
}