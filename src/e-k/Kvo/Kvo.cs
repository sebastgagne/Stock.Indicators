namespace Skender.Stock.Indicators;

public static partial class Indicator
{
    // KLINGER VOLUME OSCILLATOR
    /// <include file='./info.xml' path='indicator/*' />
    ///
    public static IEnumerable<KvoResult> GetKvo<TQuote>(
        this IEnumerable<TQuote> quotes,
        int fastPeriods = 34,
        int slowPeriods = 55,
        int signalPeriods = 13)
        where TQuote : IQuote
    {
        // convert quotes
        List<QuoteD> quotesList = quotes.ConvertToList();

        // check parameter arguments
        ValidateKlinger(fastPeriods, slowPeriods, signalPeriods);

        // initialize
        int length = quotesList.Count;
        List<KvoResult> results = new(length);

        double[] hlc = new double[length];          // trend basis
        double[] t = new double[length];            // trend direction
        double[] dm = new double[length];           // daily measurement
        double[] cm = new double[length];           // cumulative measurement
        double?[] vf = new double?[length];         // volume force (VF)
        double?[] vfFastEma = new double?[length];  // EMA of VF (short-term)
        double?[] vfSlowEma = new double?[length];  // EMA of VP (long-term)

        // EMA multipliers
        double kFast = 2d / (fastPeriods + 1);
        double kSlow = 2d / (slowPeriods + 1);
        double kSignal = 2d / (signalPeriods + 1);

        // roll through quotes
        for (int i = 0; i < length; i++)
        {
            QuoteD q = quotesList[i];
            int index = i + 1;

            KvoResult r = new()
            {
                Date = q.Date
            };
            results.Add(r);

            // trend basis comparator
            hlc[i] = q.High + q.Low + q.Close;

            // daily measurement
            dm[i] = q.High - q.Low;

            if (i <= 0)
            {
                continue;
            }

            // trend direction
            t[i] = (hlc[i] > hlc[i - 1]) ? 1 : -1;

            if (i <= 1)
            {
                cm[i] = 0;
                continue;
            }

            // cumulative measurement
            cm[i] = (t[i] == t[i - 1]) ?
                    (cm[i - 1] + dm[i]) : (dm[i - 1] + dm[i]);

            // volume force (VF)
            vf[i] = (dm[i] == cm[i] || q.Volume == 0) ? 0
                : (dm[i] == 0) ? q.Volume * 2d * t[i] * 100d
                : (cm[i] != 0) ? q.Volume * Math.Abs(2d * ((dm[i] / cm[i]) - 1)) * t[i] * 100d
                : vf[i - 1];

            // fast-period EMA of VF
            if (index > fastPeriods + 2)
            {
                vfFastEma[i] = (vf[i] * kFast) + (vfFastEma[i - 1] * (1 - kFast));
            }
            else if (index == fastPeriods + 2)
            {
                double? sum = 0;
                for (int p = 2; p <= i; p++)
                {
                    sum += vf[p];
                }

                vfFastEma[i] = sum / fastPeriods;
            }

            // slow-period EMA of VF
            if (index > slowPeriods + 2)
            {
                vfSlowEma[i] = (vf[i] * kSlow) + (vfSlowEma[i - 1] * (1 - kSlow));
            }
            else if (index == slowPeriods + 2)
            {
                double? sum = 0;
                for (int p = 2; p <= i; p++)
                {
                    sum += vf[p];
                }

                vfSlowEma[i] = sum / slowPeriods;
            }

            // Klinger Oscillator
            if (index >= slowPeriods + 2)
            {
                r.Oscillator = vfFastEma[i] - vfSlowEma[i];

                // Signal
                if (index > slowPeriods + signalPeriods + 1)
                {
                    r.Signal = (r.Oscillator * kSignal)
                        + (results[i - 1].Signal * (1 - kSignal));
                }
                else if (index == slowPeriods + signalPeriods + 1)
                {
                    double? sum = 0;
                    for (int p = slowPeriods + 1; p <= i; p++)
                    {
                        sum += results[p].Oscillator;
                    }

                    r.Signal = sum / signalPeriods;
                }
            }
        }

        return results;
    }

    // parameter validation
    private static void ValidateKlinger(
        int fastPeriods,
        int slowPeriods,
        int signalPeriods)
    {
        // check parameter arguments
        if (fastPeriods <= 2)
        {
            throw new ArgumentOutOfRangeException(nameof(fastPeriods), fastPeriods,
                "Fast (short) Periods must be greater than 2 for Klinger Oscillator.");
        }

        if (slowPeriods <= fastPeriods)
        {
            throw new ArgumentOutOfRangeException(nameof(slowPeriods), slowPeriods,
                "Slow (long) Periods must be greater than Fast Periods for Klinger Oscillator.");
        }

        if (signalPeriods <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(signalPeriods), signalPeriods,
                "Signal Periods must be greater than 0 for Klinger Oscillator.");
        }
    }
}
