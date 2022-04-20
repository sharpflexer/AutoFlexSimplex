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
    public class PriceLevel : AnalyzableBase<IOhlcv, AnalyzableTick<decimal?>>
    {
        //Как вариант, делаем список для конечных значений индикаторы
        private List<decimal> result;
        public PriceLevel(IEnumerable<IOhlcv> inputs, decimal percent) : base(inputs)
        {
            //Выполняем дальнейшие преобразования исходя из ТЗ
            //Пример 1:
            result = inputs.Select(i => i.Close).ToList();
            result = result.Zip(result, (x, y) => x + (y * percent / 100)).ToList();
        }

        // Это типа метод для высчитывания значения.
        protected override AnalyzableTick<decimal?> ComputeByIndexImpl(IReadOnlyList<IOhlcv> mappedInputs, int index)
        {
            return new AnalyzableTick<decimal?>(mappedInputs[index].DateTime, result[index]);
        }
    }
}