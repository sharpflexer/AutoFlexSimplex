using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace StrategyService.Model.Xml.LogicContainers
{
    public class Disjunction
    {
        [XmlElement("Disjunction")]
        public List<Disjunction> Disjunctions { get; set; }
        [XmlElement("Сonjunction")]
        public List<Сonjunction> Сonjunctions { get; set; }
        [XmlElement("Term")]

        public List<Term> Terms = new List<Term>();

        public Disjunction() { }

        internal bool Execute(List<OutputIndicatorBox> outputIndicators, Dictionary<string, decimal> constants)
        {
            bool conjuction = true;
            bool disjunction = true;
            bool term = true;
            if (Сonjunctions.Count > 0) conjuction = Сonjunctions.TrueForAll(element => element.Execute(outputIndicators, constants));
            if (Disjunctions.Count > 0) disjunction = Disjunctions.Exists(element => element.Execute(outputIndicators, constants));
            if (Terms.Count > 0) term = Terms.Exists(element => element.Execute(outputIndicators, constants));
            return conjuction || disjunction || term;
        }
    }
}
