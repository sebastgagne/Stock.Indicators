namespace Skender.Stock.Indicators;
#nullable disable

public static partial class Indicator
{
    // STANDARD DEVIATION
    /// <include file='./info.xml' path='indicator/*' />
    ///
    public static IEnumerable<StdDevResult> GetStdDev<TQuote>(
        this IEnumerable<TQuote> quotes,
        int lookbackPeriods,
        int? smaPeriods = null)
        where TQuote : IQuote
    {
        // convert quotes
        List<BasicD> bdList = quotes.ConvertToBasic(CandlePart.Close);

        // calculate
        return CalcStdDev(bdList, lookbackPeriods, smaPeriods);
    }

    // internals
    private static List<StdDevResult> CalcStdDev(
        List<BasicD> bdList, int lookbackPeriods, int? smaPeriods = null)
    {
        // check parameter arguments
        ValidateStdDev(lookbackPeriods, smaPeriods);

        // initialize
        List<StdDevResult> results = new(bdList.Count);

        // roll through quotes
        for (int i = 0; i < bdList.Count; i++)
        {
            BasicD bd = bdList[i];
            int index = i + 1;

            StdDevResult result = new()
            {
                Date = bd.Date,
            };

            if (index >= lookbackPeriods)
            {
                double[] periodValues = new double[lookbackPeriods];
                double sum = 0;
                int n = 0;

                for (int p = index - lookbackPeriods; p < index; p++)
                {
                    BasicD d = bdList[p];
                    periodValues[n] = d.Value;
                    sum += d.Value;
                    n++;
                }

                double periodAvg = sum / lookbackPeriods;

                result.StdDev = Functions.StdDev(periodValues);
                result.Mean = periodAvg;

                result.ZScore = (result.StdDev == 0) ? null
                    : (bd.Value - periodAvg) / result.StdDev;
            }

            results.Add(result);

            // optional SMA
            if (smaPeriods != null && index >= lookbackPeriods + smaPeriods - 1)
            {
                double sumSma = 0;
                for (int p = index - (int)smaPeriods; p < index; p++)
                {
                    sumSma += (double)results[p].StdDev;
                }

                result.StdDevSma = sumSma / smaPeriods;
            }
        }

        return results;
    }

    // parameter validation
    private static void ValidateStdDev(
        int lookbackPeriods,
        int? smaPeriods)
    {
        // check parameter arguments
        if (lookbackPeriods <= 1)
        {
            throw new ArgumentOutOfRangeException(nameof(lookbackPeriods), lookbackPeriods,
                "Lookback periods must be greater than 1 for Standard Deviation.");
        }

        if (smaPeriods is not null and <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(smaPeriods), smaPeriods,
                "SMA periods must be greater than 0 for Standard Deviation.");
        }
    }
}
