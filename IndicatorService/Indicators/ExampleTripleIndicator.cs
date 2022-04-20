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
    public class ExampleTripleIndicator<TInput, TOutput> : AnalyzableBase<TInput, decimal, (decimal? LowLine, decimal? MediumLine, decimal? HighLine), TOutput>
    {
        //Пишем индикаторы для расчёта данного индикатора(если такие необходимы)
        private SimpleMovingAverageByTuple _sma;
        private WeightedMovingAverageByTuple _wma;

        //Несколько возвращаемых значений
        private List<decimal?> resultLow;
        private List<decimal?> resultMedium;
        private List<decimal?> resultHigh;

        public ExampleTripleIndicator(IEnumerable<TInput> inputs, Func<TInput, decimal> inputMapper, int periodCount) : base(inputs, inputMapper)
        {
            //Инициализируем индикаторы для расчёта 
            _sma = new SimpleMovingAverageByTuple(inputs.Select(inputMapper), periodCount);
            _wma = new WeightedMovingAverageByTuple((IEnumerable<decimal?>)inputs.Select(inputMapper), periodCount);
            //Вычисляем индикаторы
            var sma = _sma.Compute();
            var wma = _wma.Compute();

            //Пример 2:
            resultLow = new List<decimal?>();
            for (int i = 0; i < sma.Count && i < wma.Count; i++)
            {
                //Теперь проверять нужно, так как совершаем операции с значением nullable типа
                //Коротко: если пишешь ".Value", то проверяй, если нет - значит нет.
                if (sma[i].HasValue && wma[i].HasValue)
                {
                    decimal? value = (decimal?)Math.Pow((double)(sma[i].Value - wma[i].Value), 2);
                    resultLow.Add(value);
                }
            }
            //Пример 3:
            resultMedium = new List<decimal?>();
            for (int i = 0; i < sma.Count && i < wma.Count; i++)
            {
                if (sma[i].HasValue && wma[i].HasValue)
                {
                    decimal? value = (decimal?)Math.Sqrt((double)(sma[i].Value * wma[i].Value));
                    resultMedium.Add(value);
                }

            }
            //Пример 4:
            resultHigh = new List<decimal?>();
            for (int i = 0; i < sma.Count && i < wma.Count; i++)
            {
                decimal? value = (sma[i] / wma[i]) - (sma[i] * wma[i]);
                resultHigh.Add(value);
            }
        }
        //Пример 2: для трёх
        protected override (decimal? LowLine, decimal? MediumLine, decimal? HighLine) ComputeByIndexImpl(IReadOnlyList<decimal> mappedInputs, int index)
        {        
            return (resultLow[index], resultMedium[index], resultHigh[index]);
        }
    }

    //Это типо какой-то класс-обёртка для decimal
    public class ExampleTripleIndicatorByTuple : ExampleTripleIndicator<decimal, (decimal? LowLine, decimal? MediumLine, decimal? HighLine)>
    {
        public ExampleTripleIndicatorByTuple(IEnumerable<decimal> inputs, int periodCount)
            : base(inputs, i => i, periodCount)
        {
        }
    }

    public class ExampleTripleIndicator : ExampleTripleIndicator<IOhlcv, AnalyzableTick<(decimal? LowLine, decimal? MediumLine, decimal? HighLine)>>
    {
        public ExampleTripleIndicator(IEnumerable<IOhlcv> inputs, int periodCount)
            : base(inputs, i => i.Close, periodCount)
        {
        }
    }
}
