using System;
using System.Collections.Generic;
using System.Linq;
using Trady.Analysis;
using Trady.Analysis.Indicator;
using Trady.Analysis.Infrastructure;
using Trady.Core.Infrastructure;

namespace IndicatorService.Indicators
{
    public class ValueWeightedMovingAverage : AnalyzableBase<IOhlcv, IOhlcv, decimal?, AnalyzableTick<decimal?>>
    {
        //Пишем индикаторы для расчёта данного индикатора(если такие необходимы)
        private SimpleMovingAverageByTuple _smaCloseVolume;
        private SimpleMovingAverageByTuple _smaVolume;
        //Как вариант, делаем список для конечных значений индикаторы
        private List<decimal?> result;

        List<decimal> close;
        List<decimal> volume;


        public ValueWeightedMovingAverage(IEnumerable<IOhlcv> inputs, int periodCount) : base(inputs, i=>i)
        {
            close = new List<decimal>(inputs.Select(i => i.Close));
            volume = new List<decimal>(inputs.Select(i => i.Volume));
            List<decimal> closeVolume = close.Zip(volume, (x, y) => x * y).ToList();
            _smaCloseVolume = new SimpleMovingAverageByTuple(closeVolume, periodCount);
            _smaVolume = new SimpleMovingAverageByTuple(volume, periodCount);
            var smaCloseVolume = _smaCloseVolume.Compute();
            var smaVolume = _smaVolume.Compute();
            result = smaCloseVolume.Zip(smaVolume, (x, y) => y != 0 ? x / y : 0).ToList();

        }

        protected override decimal? ComputeByIndexImpl(IReadOnlyList<IOhlcv> mappedInputs, int index)
        {
            return result[index];
        }
    }

}
