using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndicatorService
{
    [Serializable]
    public class InputIndicatorBox
    {
        public string codename;
        public string name;
        public int candleNumber;
        public List<decimal> prm;//коллекция ключ-знaчение
    }
}
