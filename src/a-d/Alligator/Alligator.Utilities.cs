namespace Skender.Stock.Indicators;

public static partial class Indicator
{
    // remove recommended periods
    /// <include file='../../_common/Results/info.xml' path='info/type[@name="Prune"]/*' />
    ///
    public static IEnumerable<AlligatorResult> RemoveWarmupPeriods(
        this IEnumerable<AlligatorResult> results)
    {
        int removePeriods = results
          .ToList()
          .FindIndex(x => x.Jaw != null) + 251;

        return results.Remove(removePeriods);
    }
}
