namespace Skender.Stock.Indicators;

public static partial class Indicator
{
    // remove recommended periods
    /// <include file='../../_common/Results/info.xml' path='info/type[@name="Prune"]/*' />
    ///
    public static IEnumerable<TrixResult> RemoveWarmupPeriods(
        this IEnumerable<TrixResult> results)
    {
        int n3 = results
            .ToList()
            .FindIndex(x => x.Trix != null) + 2;

        return results.Remove(n3 + 250);
    }
}
