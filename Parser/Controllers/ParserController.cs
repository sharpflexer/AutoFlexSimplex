using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parser.Service.Interface;
using System.Collections.Generic;

namespace Parser.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ParserController : ControllerBase
    {
        private readonly IParsingStrategyService parsingStrategyService;
        public ParserController(IParsingStrategyService strategyParser)
        {
            this.parsingStrategyService = strategyParser;
        }

        [HttpGet]
        public string GetStrategy(string name = "Strategy1")
        {
            var result = parsingStrategyService.GetStrategy(name);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet]
        public IEnumerable<string> GetNamesStrategies()
        {
            return parsingStrategyService.GetNamesStrategies();
        }
    }
}
