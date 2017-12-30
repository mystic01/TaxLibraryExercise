using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxLibraryExercise
{
    public class TaxHelper
    {
        public static void Main()
        {
        }

        public static decimal GetTaxResult(decimal income)
        {
            return TaxCalculator.Instance.Calculator(income);
        }
    }

    public class TaxTable
    {
        public readonly int? UpperBound;
        public readonly decimal Rate;

        public TaxTable(int? upperBound, decimal rate)
        {
            UpperBound = upperBound;
            Rate = rate;
        }
    }

    public class TaxCalculator
    {
        private List<TaxTable> _taxTableList = null;
        private TaxCalculator()
        {
            //Load From files
            _taxTableList = new List<TaxTable>
            {
                new TaxTable(540000, 0.05m),
                new TaxTable(1210000, 0.12m),
                new TaxTable(2420000, 0.2m),
                new TaxTable(4530000, 0.3m),
                new TaxTable(10310000, 0.4m),
                new TaxTable(null, 0.5m),
            };
        }

        private static object _syncRoot = new object();
        private static TaxCalculator _instance;
        public static TaxCalculator Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new TaxCalculator();
                        }
                    }
                }
                return _instance;
            }
        }

        public decimal Calculator(decimal income)
        {
            decimal taxResult = 0;
            var taxPaid = 0;
            foreach (var taxTable in _taxTableList)
            {
                if (taxTable.UpperBound != null && income >= taxTable.UpperBound)
                {
                    taxResult += (taxTable.UpperBound.Value - taxPaid) * taxTable.Rate;
                    taxPaid = taxTable.UpperBound.Value;
                }
                else
                {
                    taxResult += (income - taxPaid) * taxTable.Rate;
                    break;
                }
            }
            return taxResult;
        }
    }
}
