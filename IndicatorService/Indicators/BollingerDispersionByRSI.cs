using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trady.Analysis;
using Trady.Analysis.Indicator;
using Trady.Analysis.Infrastructure;
using Trady.Core.Infrastructure;

namespace IndicatorService.Indicators
{
    public class BollingerDispersionByRSI : AnalyzableBase<IOhlcv, IOhlcv, (decimal? LowLine, decimal? MediumLine, decimal? HighLine, decimal? LowDispersionLine, decimal? HihgDispersionLine), AnalyzableTick<(decimal? LowLine, decimal? MediumLine, decimal? HighLine, decimal? LowDispersionLine, decimal? HihgDispersionLine)>>
    {
        private RelativeStrengthIndexByTuple _rsi;
        private List<BinanceCandle> rsiCandles;
        private BollingerDispersion bd;

        private IReadOnlyList<(decimal? LowLine, decimal? MediumLine, decimal? HighLine, decimal? LowDispersionLine, decimal? HihgDispersionLine)> bdRSI;

        public BollingerDispersionByRSI(IEnumerable<IOhlcv> inputs, int periodCount, decimal std, decimal dispersion) : base(inputs, i => i)
        {
            _rsi = new RelativeStrengthIndexByTuple(Indicator.ConvertToNullableEnumerable(inputs.Select(i => i.Close)), 20);
            var rsi = _rsi.Compute();
            rsiCandles = new List<BinanceCandle>();
            for(int i = 0; i < inputs.Count(); i++)
            {
                BinanceCandle rsiCandle = new BinanceCandle();
                rsiCandle.Close = rsi[i].GetValueOrDefault();
                rsiCandle.Open = inputs.ElementAt(i).Open;
                rsiCandle.High = inputs.ElementAt(i).High;
                rsiCandle.Low = inputs.ElementAt(i).Low;
                rsiCandle.Volume = inputs.ElementAt(i).Volume;
                rsiCandle.DateTime = inputs.ElementAt(i).DateTime;
                rsiCandles.Add(rsiCandle);
            }
            bd = new BollingerDispersion(rsiCandles, periodCount, std, dispersion);
            IEnumerable<(decimal? LowLine, decimal? MediumLine, decimal? HighLine, decimal? LowDispersionLine, decimal? HihgDispersionLine)> bdvar = bd.Compute().Select(i => i.Tick);
            bdRSI = new List<(decimal? LowLine, decimal? MediumLine, decimal? HighLine, decimal? LowDispersionLine, decimal? HihgDispersionLine)>(bdvar);
            
        }

        protected override (decimal? LowLine, decimal? MediumLine, decimal? HighLine, decimal? LowDispersionLine, decimal? HihgDispersionLine) ComputeByIndexImpl(IReadOnlyList<IOhlcv> mappedInputs, int index)
        {
            return (bdRSI[index].LowLine, bdRSI[index].MediumLine, bdRSI[index].HighLine, bdRSI[index].LowDispersionLine, bdRSI[index].HihgDispersionLine);
        }
    }
}
