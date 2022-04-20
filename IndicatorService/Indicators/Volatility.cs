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
    public class Volatility : AnalyzableBase<IOhlcv, IOhlcv, decimal?, AnalyzableTick<decimal?>>
    {
        private AverageTrueRange atr;
        private List<decimal?> result;

        public Volatility(IEnumerable<IOhlcv> inputs, int periodCount) : base(inputs, i => i)
        {
            List<decimal> closes = inputs.Select(i => i.Close).ToList();
            this.atr = new AverageTrueRange(inputs, periodCount);
            var atr = this.atr.Compute();
            result = new List<decimal?>();
            for (int i = 0; i < atr.Count; i++)
            {
                decimal? value = atr[atr.Count - i - 1].Tick / closes[closes.Count() - i - 1] * 100;
                result.Add(value);
            }
            result.Reverse();
        }

        // Это типа метод для высчитывания значения.
        protected override decimal? ComputeByIndexImpl(IReadOnlyList<IOhlcv> mappedInputs, int index)
        {
            return result[index];
        }
    }
}
