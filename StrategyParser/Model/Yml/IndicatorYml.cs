using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyParser.Domain.Model.Yml
{
    public class IndicatorYml
    {
        public string Indicator { get; set; }
        public int Period { get; set; }
        public int CandleNumber { get; set; }
        public int? Stdv { get; set; }
        public decimal? Disp { get; set; }
    }
}
