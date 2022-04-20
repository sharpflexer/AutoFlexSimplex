using StrategyParser.Model.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace StrategyParser.Domain.Model.Xml.LogicContainers
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
    }
}
