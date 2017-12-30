namespace TaxLibraryExercise
{
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
}