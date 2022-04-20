using SharpYaml.Serialization;
using StrategyParser.Domain.Model.Xml.Indicator;
using StrategyParser.Domain.Model.Xml.LogicContainers;
using StrategyParser.Model.Xml;
using StrategyParser.Model.Yml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace StrategyParser
{
    public class RepositoryStrategy : IRepositoryStrategy
    {
        const string directoryForStrategies = "Strategy";

        private readonly XmlSerializer formatterXml = new XmlSerializer(typeof(Strategy));
        private readonly Serializer serializer = new Serializer();

        public Strategy GetStrategyBy(string directoryNameStrategy)
        {
          var strategy = GetXmlByNameStrategy(directoryNameStrategy);
          var ymlConteiner = GetYmlByNameStrategy(directoryNameStrategy);

          strategy.Constants = ymlConteiner.Parameters.Constant;
          strategy.InputIndicatorBoxes = ParsingYmlContiner(ymlConteiner);

          return strategy;                  
        } 

        private Strategy GetXmlByNameStrategy (string fileName)
        {
            using (FileStream fileStream = new FileStream(GetFileNameStrategyFrom(fileName, ".xml"), FileMode.Open))
            {
                var strategy = (Strategy)formatterXml.Deserialize(fileStream);
                return strategy;
            }
        }

        public YmlContiner GetYmlByNameStrategy(string fileName)
        {
            using (FileStream fileStream = new FileStream(GetFileNameStrategyFrom(fileName, ".yml"), FileMode.Open))
            {
                return serializer.Deserialize<YmlContiner>(fileStream);
            }
        }    
        public IEnumerable<string> GetNamesStrategy()
        {
            return Directory.GetDirectories(Path.Combine(GetPathRootFolders(), directoryForStrategies))
                    .Select(s => s.Split("\\").Last());          
        }


        public void CreateStrategy(Strategy strategy)
        {
            using (FileStream fileStream = new FileStream(GetPathRootFolders() + strategy.NameStrategy, FileMode.Create))
            {
                formatterXml.Serialize(fileStream, strategy);
            }
        }  
        private IEnumerable<InputIndicatorBox> ParsingYmlContiner(YmlContiner continer)
        {
            List<InputIndicatorBox> indicatorBoxs = new List<InputIndicatorBox>();
            var dictionaryIndicators = continer.Parameters.Indicator;

            foreach(var indicator in dictionaryIndicators)
            {
                InputIndicatorBox indicatorBox = new InputIndicatorBox();
                indicatorBox.codename = indicator.Key;
                indicatorBox.name = indicator.Value.Indicator;
                indicatorBox.candleNumber = indicator.Value.CandleNumber;
                indicatorBox.prm = new List<decimal>() { indicator.Value.Period};
                if (indicator.Value.Stdv.HasValue) indicatorBox.prm.Add(indicator.Value.Stdv.Value);
                if (indicator.Value.Disp.HasValue) indicatorBox.prm.Add(indicator.Value.Disp.Value);
                indicatorBoxs.Add(indicatorBox);
            }
            return indicatorBoxs;           
        }

        /// <summary>
        /// Получить папку текущий сборки 
        /// </summary>
        /// <returns></returns>
        private string GetPathRootFolders()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
      
        /// <summary>
        /// Получаем определенный файл
        /// </summary>
        /// <param name="directory">Целевая папка</param>       
        /// <param name="typeFile">Тип файла</param>
        /// <returns></returns>
        private string GetFileNameStrategyFrom(string directory, string typeFile)
        {
            return Directory.GetFiles(Path.Combine(GetPathRootFolders(), directoryForStrategies, directory), directory + typeFile).FirstOrDefault();
           
        }
    }
   
}
