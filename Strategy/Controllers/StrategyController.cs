using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StrategyService.Model.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace StrategyService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StrategyController : ControllerBase
    {
        private string parserPath = "https://localhost:44385/api/parser/GetStrategy?";
        private string indicatorPath = "https://localhost:44371/api/indicator/GetOutputIndicators?";

        
        [HttpGet]
        public string GetTesterResult()
        {
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < 250; i++)
            {
                string result = GetStrategyResult(250 - i);
                builder.Append(250 - i + " " + result + "\n");
            }
            return builder.ToString();
            
        }
        
        [HttpGet]
        public string GetStrategyResult(int shift = 0)
        {
            Strategy strategy = SendRequest<Strategy>(parserPath, "GET", "name=Strategy1");
            List<OutputIndicatorBox> boxes = GetIndicatorsValue(new List<InputIndicatorBox>(strategy.InputIndicatorBoxes), shift);
            StrategyResult result = strategy.Execute(boxes);
            return JsonConvert.SerializeObject(result);
        }


        private List<OutputIndicatorBox> GetIndicatorsValue(List<InputIndicatorBox> inputIndicators, int shift)
        {
            string jsonIndicators = JsonConvert.SerializeObject(inputIndicators);
            string jsonInput = "inputIndicatorBoxesJson=" + jsonIndicators + "&shift=" + shift;
            List<OutputIndicatorBox> outputBoxes = SendRequest<List<OutputIndicatorBox>>(indicatorPath, "GET", jsonInput);

            return outputBoxes;
        }
        
        //TODO: REFACTOR THIS SHIT
        private T SendRequest<T>(string path, string type, string jsonInput = "")
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(path);
            builder.Append(jsonInput);
            WebRequest request = WebRequest.Create(builder.ToString());
            request.Method = type;

            WebResponse response = request.GetResponse();

            String json = null;
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                json = responseFromServer;
            }
            // Close the response.
            response.Close();

            T result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
    }
}
