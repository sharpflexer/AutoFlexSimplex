using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace StrategyService.Model.Xml
{
    public abstract class LogicContainer
    {
        public List<LogicContainer> LogicContainers { get; set; }    
        
     
        public List<Term> Terms = new List<Term>();
        public LogicContainer(){}
    }
    
}