namespace Skender.Stock.Indicators;

public static partial class Indicator
{
    // PIVOT POINTS
    /// <include file='./info.xml' path='indicator/*' />
    ///
    public static IEnumerable<RollingPivotsResult> GetRollingPivots<TQuote>(
        this IEnumerable<TQuote> quotes,
        int windowPeriods,
        int offsetPeriods,
        PivotPointType pointType = PivotPointType.Standard)
        where TQuote : IQuote
    {
        // sort quotes
        List<TQuote> quotesList = quotes.SortToList();

        // check parameter arguments
        ValidateRollingPivots(windowPeriods, offsetPeriods);

        // initialize
        List<RollingPivotsResult> results = new(quotesList.Count);

        // roll through quotes
        for (int i = 0; i < quotesList.Count; i++)
        {
            TQuote q = quotesList[i];

            RollingPivotsResult r = new()
            {
                Date = q.Date
            };

            if (i >= windowPeriods + offsetPeriods)
            {
                // window values
                int s = i - windowPeriods - offsetPeriods;
                TQuote hi = quotesList[s];

                decimal windowHigh = hi.High;
                decimal windowLow = hi.Low;
                decimal windowClose = quotesList[i - offsetPeriods - 1].Close;

                for (int p = s; p <= i - offsetPeriods - 1; p++)
                {
                    TQuote d = quotesList[p];
                    windowHigh = (d.High > windowHigh) ? d.High : windowHigh;
                    windowLow = (d.Low < windowLow) ? d.Low : windowLow;
                }

                // pivot points
                RollingPivotsResult wp = GetPivotPoint<RollingPivotsResult>(
                        pointType, q.Open, windowHigh, windowLow, windowClose);

                r.PP = wp.PP;
                r.S1 = wp.S1;
                r.S2 = wp.S2;
                r.S3 = wp.S3;
                r.S4 = wp.S4;
                r.R1 = wp.R1;
                r.R2 = wp.R2;
                r.R3 = wp.R3;
                r.R4 = wp.R4;
            }

            results.Add(r);
        }

        return results;
    }

    // parameter validation
    private static void ValidateRollingPivots(
        int windowPeriods,
        int offsetPeriods)
    {
        // check parameter arguments
        if (windowPeriods <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(windowPeriods), windowPeriods,
                "Window periods must be greater than 0 for Rolling Pivot Points.");
        }

        if (offsetPeriods < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(offsetPeriods), offsetPeriods,
                "Offset periods must be greater than or equal to 0 for Rolling Pivot Points.");
        }
    }
}
