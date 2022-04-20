using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Trady.Analysis.Extension;
using Trady.Core.Infrastructure;

namespace IndicatorService.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class IndicatorController : ControllerBase
    {
        [HttpGet]
        public string GetOutputIndicators(string inputIndicatorBoxesJson, int shift = 0)
        {
            List<InputIndicatorBox> inputIndicatorBoxes = JsonConvert.DeserializeObject<List<InputIndicatorBox>>(inputIndicatorBoxesJson);
            Indicator indicator = new Indicator();
            IEnumerable<IOhlcv> candles = GetCandles(); //http://localhost:49464
            List<OutputIndicatorBox> value = indicator.Compute(candles, inputIndicatorBoxes, shift);// передавали indicatorBox
            return JsonConvert.SerializeObject(value);
        }
        [HttpGet]
        public decimal GetPriceNegativeLevel(int mult)
        {
            return GetPriceLevel(-1, mult);
        }
        [HttpGet]
        public decimal GetPricePositiveLevel(int mult)
        {
            return GetPriceLevel(1, mult);
        }

        private decimal GetPriceLevel(int side, int mult)
        {
            var candles = GetCandles();
            decimal close = candles.Func("pricelvl", 0)[candles.Count() - 1].Tick.Value;
            decimal vltl = candles.Func("vltl", 24)[candles.Count() - 1].Tick.Value;
            decimal level = close + side * (1 + mult * vltl) * close / 100;
            return level;
        }
        private IEnumerable<IOhlcv> GetCandles()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("https://localhost:44319/api/candles/CandlesData?");
            builder.Append("pair=BTCUSDT&");
            builder.Append("interval=" + 3);
            WebRequest request = WebRequest.Create(builder.ToString());
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

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            IEnumerable<BinanceCandle> binanceCandles = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<BinanceCandle>>(json, serializeOptions);

            Console.WriteLine("all fine");
            return binanceCandles;
        }
    }
}
