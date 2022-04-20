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
    public class BollingerDispersion : AnalyzableBase<IOhlcv, IOhlcv, (decimal? LowLine, decimal? MediumLine, decimal? HighLine, decimal? LowDispersionLine, decimal? HihgDispersionLine),  AnalyzableTick<(decimal? LowLine, decimal? MediumLine, decimal? HighLine, decimal? LowDispersionLine, decimal? HihgDispersionLine)>>
    {
        private ValueWeightedMovingAverage vwma; //TODO: Заменить на VWMA
        private StandardDeviation sd;



        private List<decimal?> low;
        private List<decimal?> medium;
        private List<decimal?> high;
        private List<decimal?> dispersionLow;
        private List<decimal?> dispersionHigh;

        public BollingerDispersion(IEnumerable<IOhlcv> inputs, int periodCount, decimal std, decimal dispersion) : base(inputs, i => i)
        {
            
            this.vwma = new ValueWeightedMovingAverage(inputs, periodCount);
            this.sd = new StandardDeviation(inputs, periodCount);

            var vwma = this.vwma.Compute();
            var sd = this.sd.Compute();

            medium = new List<decimal?>(vwma.Select(i => i.Tick));

            List<decimal?> deviation = new List<decimal?>();
            foreach (AnalyzableTick<decimal?> element in sd)
            {
                deviation.Add(std * element.Tick);
            }
            low = medium.Zip(deviation, (x, y) => x - y).ToList();
            high = medium.Zip(deviation, (x, y) => x + y).ToList();
            List<decimal?> difference = high.Zip(low, (x, y) => (x - y) * dispersion).ToList();
            dispersionLow = medium.Zip(difference, (x, y) => x - y).ToList();
            dispersionHigh = medium.Zip(difference, (x, y) => x + y).ToList();

        }

        protected override (decimal? LowLine, decimal? MediumLine, decimal? HighLine, decimal? LowDispersionLine, decimal? HihgDispersionLine) ComputeByIndexImpl(IReadOnlyList<IOhlcv> mappedInputs, int index)
        {
            return (low[index], medium[index], high[index], dispersionLow[index], dispersionHigh[index]);
        }
    }
}
