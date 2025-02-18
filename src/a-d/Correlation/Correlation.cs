namespace Skender.Stock.Indicators;

public static partial class Indicator
{
    // CORRELATION COEFFICIENT
    /// <include file='./info.xml' path='indicator/*' />
    ///
    public static IEnumerable<CorrResult> GetCorrelation<TQuote>(
        this IEnumerable<TQuote> quotesA,
        IEnumerable<TQuote> quotesB,
        int lookbackPeriods)
        where TQuote : IQuote
    {
        // convert quotes
        List<BasicD> bdListA = quotesA.ConvertToBasic(CandlePart.Close);
        List<BasicD> bdListB = quotesB.ConvertToBasic(CandlePart.Close);

        // check parameter arguments
        ValidateCorrelation(quotesA, quotesB, lookbackPeriods);

        // initialize
        List<CorrResult> results = new(bdListA.Count);

        // roll through quotes
        for (int i = 0; i < bdListA.Count; i++)
        {
            BasicD a = bdListA[i];
            BasicD b = bdListB[i];
            int index = i + 1;

            if (a.Date != b.Date)
            {
                throw new InvalidQuotesException(nameof(quotesA), a.Date,
                    "Date sequence does not match.  Correlation requires matching dates in provided histories.");
            }

            CorrResult r = new()
            {
                Date = a.Date
            };

            // calculate correlation
            if (index >= lookbackPeriods)
            {
                double[] dataA = new double[lookbackPeriods];
                double[] dataB = new double[lookbackPeriods];
                int z = 0;

                for (int p = index - lookbackPeriods; p < index; p++)
                {
                    dataA[z] = bdListA[p].Value;
                    dataB[z] = bdListB[p].Value;

                    z++;
                }

                r.CalcCorrelation(dataA, dataB);
            }

            results.Add(r);
        }

        return results;
    }

    // calculate correlation
    private static void CalcCorrelation(
        this CorrResult r,
        double[] dataA,
        double[] dataB)
    {
        int length = dataA.Length;
        double sumA = 0;
        double sumB = 0;
        double sumA2 = 0;
        double sumB2 = 0;
        double sumAB = 0;

        for (int i = 0; i < length; i++)
        {
            double a = dataA[i];
            double b = dataB[i];

            sumA += a;
            sumB += b;
            sumA2 += a * a;
            sumB2 += b * b;
            sumAB += a * b;
        }

        double avgA = sumA / length;
        double avgB = sumB / length;
        double avgA2 = sumA2 / length;
        double avgB2 = sumB2 / length;
        double avgAB = sumAB / length;

        r.VarianceA = avgA2 - (avgA * avgA);
        r.VarianceB = avgB2 - (avgB * avgB);
        r.Covariance = avgAB - (avgA * avgB);

        double divisor = Math.Sqrt((double)(r.VarianceA * r.VarianceB));

        r.Correlation = (divisor == 0) ? null : r.Covariance / divisor;

        r.RSquared = r.Correlation * r.Correlation;
    }

    // parameter validation
    private static void ValidateCorrelation<TQuote>(
        IEnumerable<TQuote> quotesA,
        IEnumerable<TQuote> quotesB,
        int lookbackPeriods)
        where TQuote : IQuote
    {
        // check parameter arguments
        if (lookbackPeriods <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(lookbackPeriods), lookbackPeriods,
                "Lookback periods must be greater than 0 for Correlation.");
        }

        // check quotes
        if (quotesA.Count() != quotesB.Count())
        {
            throw new InvalidQuotesException(
                nameof(quotesB),
                "B quotes should have at least as many records as A quotes for Correlation.");
        }
    }
}
