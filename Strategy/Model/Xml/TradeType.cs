using StrategyService.Model.Xml.LogicContainers;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace StrategyService.Model.Xml
{
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

        public bool Execute(List<OutputIndicatorBox> outputIndicators, Dictionary<string, decimal> constants)
        {
            bool conjuction = true;
            bool disjunction = true;
            if(logicContainerConjunct.Count > 0) conjuction = logicContainerConjunct.TrueForAll(element => element.Execute(outputIndicators, constants));
            if(logicContainerDisjunction.Count > 0) disjunction = logicContainerDisjunction.Exists(element => element.Execute(outputIndicators, constants));
            return conjuction && disjunction;
        }
    }
}