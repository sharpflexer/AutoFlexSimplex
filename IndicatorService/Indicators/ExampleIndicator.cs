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
    public class ExampleIndicator : AnalyzableBase<IOhlcv, AnalyzableTick<decimal?>>
    {
        //Пишем индикаторы для расчёта данного индикатора(если такие необходимы)
        private SimpleMovingAverageByTuple _sma;
        private WeightedMovingAverageByTuple _wma;
        //Как вариант, делаем список для конечных значений индикаторы
        private List<decimal?> result;

        public ExampleIndicator(IEnumerable<IOhlcv> inputs, int periodCount) : base(inputs)
        {
            //Инициализируем индикаторы для расчёта
            _sma = new SimpleMovingAverageByTuple(inputs.Select(i => i.Close).ToList(), periodCount);
            _wma = new WeightedMovingAverageByTuple((IEnumerable<decimal?>)inputs.Select(i => i.Close).ToList(), periodCount);
            //Вычисляем индикаторы
            var sma = _sma.Compute();
            var wma = _wma.Compute();
            //Выполняем дальнейшие преобразования исходя из ТЗ
            //Пример 1:
            result = new List<decimal?>();
            for(int i = 0; i < sma.Count && i < wma.Count; i++)
            {
                //Проверять, имеют ли nullable переменные значения null НЕ НУЖНО!
                decimal? value = (sma[i] + wma[i]) / 2;
                result.Add(value);
            }
        }

        // Это типа метод для высчитывания значения.
        protected override AnalyzableTick<decimal?> ComputeByIndexImpl(IReadOnlyList<IOhlcv> mappedInputs, int index)
        {
            return new AnalyzableTick<decimal?>(mappedInputs[index].DateTime, result[index]);
        }
    }
}