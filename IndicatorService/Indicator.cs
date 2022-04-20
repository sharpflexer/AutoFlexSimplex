using IndicatorService.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trady.Analysis.Extension;
using Trady.Analysis.Indicator;
using Trady.Analysis.Infrastructure;
using Trady.Core.Infrastructure;

namespace IndicatorService
{
    class Indicator
    {
        static Indicator()
        {
            FuncRegistry.Register("sma", (c, i, p, ctx) => ctx.Get<SimpleMovingAverage>(p[0])[i].Tick);
            FuncRegistry.Register("wma", (c, i, p, ctx) => ctx.Get<WeightedMovingAverage>(p[0])[i].Tick);
            FuncRegistry.Register("ema", (c, i, p, ctx) => ctx.Get<ExponentialMovingAverage>(p[0])[i].Tick);
            FuncRegistry.Register("rsi", (c, i, p, ctx) => ctx.Get<RelativeStrengthIndex>(p[0])[i].Tick);
            FuncRegistry.Register("atr", (c, i, p, ctx) => ctx.Get<AverageTrueRange>(p[0])[i].Tick);

            FuncRegistry.Register("bb_low", (c, i, p, ctx) => ctx.Get<BollingerBands>(p[0], p[1])[i].Tick.LowerBand);
            FuncRegistry.Register("bb_medium", (c, i, p, ctx) => ctx.Get<BollingerBands>(p[0], p[1])[i].Tick.MiddleBand);
            FuncRegistry.Register("bb_upper", (c, i, p, ctx) => ctx.Get<BollingerBands>(p[0], p[1])[i].Tick.UpperBand);
            //Custom
            //Volatility
            FuncRegistry.Register("vltl", (c, i, p, ctx) => ctx.Get<Volatility>(p[0])[i].Tick);
            //Price Level
            FuncRegistry.Register("pricelvl", (c, i, p, ctx) => ctx.Get<PriceLevel>(p[0])[i].Tick);
            //Exit Price by Volatility
            FuncRegistry.Register("epv", (c, i, p, ctx) => ctx.Get<ExitPriceVolatility>(p[0])[i].Tick);
            //Value Weighted Moving Average
            FuncRegistry.Register("vwma", (c, i, p, ctx) => ctx.Get<ValueWeightedMovingAverage>(p[0])[i].Tick);
            //Weighted Bollinger Bands
            FuncRegistry.Register("wbb_low", (c, i, p, ctx) => ctx.Get<WeightedBolingerBands>(p[0], p[1])[i].Tick.LowLine);
            FuncRegistry.Register("wbb_medium", (c, i, p, ctx) => ctx.Get<WeightedBolingerBands>(p[0], p[1])[i].Tick.MediumLine);
            FuncRegistry.Register("wbb_high", (c, i, p, ctx) => ctx.Get<WeightedBolingerBands>(p[0], p[1])[i].Tick.HighLine);
            //Bollinger Dispersion by RSI
            FuncRegistry.Register("bdrsi_low", (c, i, p, ctx) => ctx.Get<BollingerDispersionByRSI>(p[0], p[1], p[2])[i].Tick.LowLine);
            FuncRegistry.Register("bdrsi_medium", (c, i, p, ctx) => ctx.Get<BollingerDispersionByRSI>(p[0], p[1], p[2])[i].Tick.MediumLine);
            FuncRegistry.Register("bdrsi_high", (c, i, p, ctx) => ctx.Get<BollingerDispersionByRSI>(p[0], p[1], p[2])[i].Tick.HighLine);
            FuncRegistry.Register("bdrsi_lowdisp", (c, i, p, ctx) => ctx.Get<BollingerDispersionByRSI>(p[0], p[1], p[2])[i].Tick.LowDispersionLine);
            FuncRegistry.Register("bdrsi_highdisp", (c, i, p, ctx) => ctx.Get<BollingerDispersionByRSI>(p[0], p[1], p[2])[i].Tick.HihgDispersionLine);
        }

        public List<OutputIndicatorBox> Compute(IEnumerable<IOhlcv> candles, List<InputIndicatorBox>box, int shift)
        {
            List<OutputIndicatorBox> outBoxes = new List<OutputIndicatorBox>();
            for (int i = 0; i < box.Count; i++)                                          
            {
                var somedeepshit = box[i].name;
                var somedeepshit2 = box[i].prm.ToArray();
                var someshit = candles.Func(somedeepshit, somedeepshit2);
                var lastModifiedSmaValue = someshit[candles.Count() - box[i].candleNumber - 1 - shift];
                OutputIndicatorBox outBox = new OutputIndicatorBox();
                outBox.codename = box[i].codename;
                outBox.name = box[i].name;
                outBox.candleNumber = box[i].candleNumber;
                outBox.value = lastModifiedSmaValue.Tick.Value;
                outBoxes.Add(outBox);
            }
            return outBoxes;
        }

        public static IEnumerable<decimal?> ConvertToNullableEnumerable(IEnumerable<decimal> inputList)
        {
            IEnumerable<decimal?> result = new List<decimal?>();
            foreach (decimal element in inputList)
            {
                decimal? resultElement = element;
                result = result.Append(resultElement);
            }
            return result;
        }
    }
}
