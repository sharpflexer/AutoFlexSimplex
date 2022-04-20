using Parser.Service.Interface;
using StrategyParser;
using StrategyParser.Model.Xml;
using System.Collections.Generic;

namespace Parser.Service
{
    public class ParsingStrategyService : IParsingStrategyService
    {
        private readonly IRepositoryStrategy repositoryStrategy;

        public ParsingStrategyService(IRepositoryStrategy repositoryStrategy)
        {
            this.repositoryStrategy = repositoryStrategy;
        }

        public void CreateStrategy(Strategy strategy)
        {
            repositoryStrategy.CreateStrategy(strategy);
        }

        public IEnumerable<string> GetNamesStrategies()
        {
            return repositoryStrategy.GetNamesStrategy();
        }

        public Strategy GetStrategy(string nameStrategy)
        {
            var strategy = repositoryStrategy.GetStrategyBy(nameStrategy);
            return strategy;
        }

    }
}
