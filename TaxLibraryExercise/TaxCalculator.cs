using System.Collections.Generic;

namespace TaxLibraryExercise
{
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
            var calculatedIncome = 0;
            foreach (var taxTable in _taxTableList)
            {
                taxResult += GetIntervalValue(income, calculatedIncome, taxTable.UpperBound) * taxTable.Rate;

                if (taxTable.UpperBound != null)
                    calculatedIncome = taxTable.UpperBound.Value;

                if (calculatedIncome >= income)
                    break;
            }
            return taxResult;
        }

        private decimal GetIntervalValue(decimal income, decimal calculatedIncome, int? upperBound)
        {
            if (upperBound != null && income >= upperBound.Value)
                return upperBound.Value - calculatedIncome;
            else
                return income - calculatedIncome;
        }
    }
}