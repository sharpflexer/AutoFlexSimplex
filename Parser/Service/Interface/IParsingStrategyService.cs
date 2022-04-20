using StrategyParser.Model.Xml;
using System.Collections.Generic;

namespace Parser.Service.Interface
{
    public interface IParsingStrategyService
    {
        public IEnumerable<string> GetNamesStrategies();
        public Strategy GetStrategy(string nameStrategy);

        public void CreateStrategy(Strategy strategy);
    }
}
