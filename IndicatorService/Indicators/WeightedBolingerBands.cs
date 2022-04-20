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
    //Пример с тремя возвращаемыми значениями
    public class WeightedBolingerBands<TInput, TOutput> : AnalyzableBase<TInput, decimal, (decimal? LowLine, decimal? MediumLine, decimal? HighLine), TOutput>
    {
        //Пишем индикаторы для расчёта данного индикатора(если такие необходимы)
        private StandardDeviationByTuple _stdev;
        private WeightedMovingAverageByTuple _wma;

        //Несколько возвращаемых значений
        private List<decimal?> resultLow;
        private List<decimal?> resultMedium;
        private List<decimal?> resultHigh;



        public WeightedBolingerBands(IEnumerable<TInput> inputs, Func<TInput, decimal> inputMapper, int periodCount, decimal mult) : base(inputs, inputMapper)
        {
            //Инициализируем индикаторы для расчёта 
            _stdev = new StandardDeviationByTuple(inputs.Select(inputMapper), periodCount);
            IEnumerable<decimal> inputsList = inputs.Select(inputMapper);
            _wma = new WeightedMovingAverageByTuple(Indicator.ConvertToNullableEnumerable(inputsList), periodCount);
            //Вычисляем индикаторы
            var stdev = _stdev.Compute();
            var wma = _wma.Compute();

            //Пример 2:
            resultLow = new List<decimal?>();
            resultMedium = wma.ToList();

            List<decimal?> deviation = new List<decimal?>();
            deviation = stdev.Zip(stdev, (x, y) => x * mult).ToList();
            
            resultLow = resultMedium.Zip(deviation, (x, y) => x - y).ToList();
            resultHigh = resultMedium.Zip(deviation, (x, y) => x + y).ToList();
            
        }
        //Пример 2: для трёх
        protected override (decimal? LowLine, decimal? MediumLine, decimal? HighLine) ComputeByIndexImpl(IReadOnlyList<decimal> mappedInputs, int index)
        {
            return (resultLow[index], resultMedium[index], resultHigh[index]);
        }


    }


    //Это типо какой-то класс-обёртка для decimal
    public class WeightedBolingerBandsByTuple : WeightedBolingerBands<decimal, (decimal? LowLine, decimal? MediumLine, decimal? HighLine)>
    {
        public WeightedBolingerBandsByTuple(IEnumerable<decimal> inputs, int periodCount, decimal mult)
            : base(inputs, i => i, periodCount, mult)
        {
        }
    }

    public class WeightedBolingerBands : WeightedBolingerBands<IOhlcv, AnalyzableTick<(decimal? LowLine, decimal? MediumLine, decimal? HighLine)>>
    {
        public WeightedBolingerBands(IEnumerable<IOhlcv> inputs, int periodCount, decimal mult)
            : base(inputs, i => i.Close, periodCount, mult)
        {
        }
    }
}
