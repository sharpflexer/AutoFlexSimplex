
using StrategyParser.Model.Xml;
using System.Collections.Generic;

namespace StrategyParser
{
    public interface IRepositoryStrategy
    {
        public Strategy GetStrategyBy(string fileName);       
        public IEnumerable<string> GetNamesStrategy();       
        public void CreateStrategy(Strategy strategy);
    }
}
