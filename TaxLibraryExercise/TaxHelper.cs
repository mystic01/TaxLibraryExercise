namespace TaxLibraryExercise
{
    public class TaxHelper
    {
        public static decimal GetTaxResult(decimal income)
        {
            return TaxCalculator.Instance.Calculate(income);
        }
    }
}