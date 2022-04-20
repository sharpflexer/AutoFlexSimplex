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
    public class ExitPriceVolatility : AnalyzableBase<IOhlcv, AnalyzableTick<decimal?>>
    {
        //Пишем индикаторы для расчёта данного индикатора(если такие необходимы)
        private PriceLevel _pl;
        private Volatility _vol;
        //Как вариант, делаем список для конечных значений индикаторы
        private List<decimal?> result;

        public ExitPriceVolatility(IEnumerable<IOhlcv> inputs, int periodCount, decimal percent) : base(inputs)
        {
            //Инициализируем индикаторы для расчёта
            _pl = new PriceLevel(inputs,percent);
            _vol = new Volatility(inputs,periodCount);
            //Вычисляем индикаторы
            var pl = _pl.Compute();
            var vol = _vol.Compute();
            
            result = pl.Zip(vol, (x,y) => x.Tick + y.Tick).ToList();
            
        }

        // Это типа метод для высчитывания значения.
        protected override AnalyzableTick<decimal?> ComputeByIndexImpl(IReadOnlyList<IOhlcv> mappedInputs, int index)
        {
            return new AnalyzableTick<decimal?>(mappedInputs[index].DateTime, result[index]);
        }
    }
}