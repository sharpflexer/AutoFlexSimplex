using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyParser.Domain.Model.Xml.Indicator
{
    public class InputIndicatorBox
    {
        public string codename;
        public string name;
        public int candleNumber;
        public List<decimal> prm;//коллекция ключ-знaчение
    }
}
