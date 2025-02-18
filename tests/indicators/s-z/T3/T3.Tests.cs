using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skender.Stock.Indicators;

namespace Internal.Tests;

[TestClass]
public class T3 : TestBase
{
    [TestMethod]
    public void Standard()
    {
        List<T3Result> results = quotes.GetT3(5, 0.7).ToList();

        // assertions

        // proper quantities
        // should always be the same number of results as there is quotes
        Assert.AreEqual(502, results.Count);
        Assert.AreEqual(478, results.Where(x => x.T3 != null).Count());

        // sample values
        T3Result r1 = results[23];
        Assert.IsNull(r1.T3);

        T3Result r2 = results[24];
        Assert.AreEqual(215.9343m, Math.Round((decimal)r2.T3, 4));

        T3Result r3 = results[44];
        Assert.AreEqual(224.9412m, Math.Round((decimal)r3.T3, 4));

        T3Result r4 = results[149];
        Assert.AreEqual(235.8851m, Math.Round((decimal)r4.T3, 4));

        T3Result r5 = results[249];
        Assert.AreEqual(257.8735m, Math.Round((decimal)r5.T3, 4));

        T3Result r6 = results[501];
        Assert.AreEqual(238.9308m, Math.Round((decimal)r6.T3, 4));
    }

    [TestMethod]
    public void BadData()
    {
        IEnumerable<T3Result> r = Indicator.GetT3(badQuotes);
        Assert.AreEqual(502, r.Count());
    }

    [TestMethod]
    public void NoQuotes()
    {
        IEnumerable<T3Result> r0 = noquotes.GetT3();
        Assert.AreEqual(0, r0.Count());

        IEnumerable<T3Result> r1 = onequote.GetT3();
        Assert.AreEqual(1, r1.Count());
    }

    [TestMethod]
    public void Removed()
    {
        List<T3Result> results = quotes.GetT3(5, 0.7)
            .RemoveWarmupPeriods()
            .ToList();

        // assertions
        Assert.AreEqual(502 - ((6 * (5 - 1)) + 250), results.Count);

        T3Result last = results.LastOrDefault();
        Assert.AreEqual(238.9308m, Math.Round((decimal)last.T3, 4));
    }

    [TestMethod]
    public void Exceptions()
    {
        // bad lookback period
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            Indicator.GetT3(quotes, 0));

        // bad volume factor
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            Indicator.GetT3(quotes, 25, 0));
    }
}
