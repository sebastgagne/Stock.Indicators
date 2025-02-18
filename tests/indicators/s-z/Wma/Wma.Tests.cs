using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skender.Stock.Indicators;

namespace Internal.Tests;

[TestClass]
public class Wma : TestBase
{
    [TestMethod]
    public void Standard()
    {
        List<WmaResult> results = quotes.GetWma(20).ToList();

        // assertions

        // proper quantities
        // should always be the same number of results as there is quotes
        Assert.AreEqual(502, results.Count);
        Assert.AreEqual(483, results.Where(x => x.Wma != null).Count());

        // sample values
        WmaResult r1 = results[149];
        Assert.AreEqual(235.5253m, Math.Round((decimal)r1.Wma, 4));

        WmaResult r2 = results[501];
        Assert.AreEqual(246.5110m, Math.Round((decimal)r2.Wma, 4));
    }

    [TestMethod]
    public void BadData()
    {
        IEnumerable<WmaResult> r = Indicator.GetWma(badQuotes, 15);
        Assert.AreEqual(502, r.Count());
    }

    [TestMethod]
    public void NoQuotes()
    {
        IEnumerable<WmaResult> r0 = noquotes.GetWma(5);
        Assert.AreEqual(0, r0.Count());

        IEnumerable<WmaResult> r1 = onequote.GetWma(5);
        Assert.AreEqual(1, r1.Count());
    }

    [TestMethod]
    public void Removed()
    {
        List<WmaResult> results = quotes.GetWma(20)
            .RemoveWarmupPeriods()
            .ToList();

        // assertions
        Assert.AreEqual(502 - 19, results.Count);

        WmaResult last = results.LastOrDefault();
        Assert.AreEqual(246.5110m, Math.Round((decimal)last.Wma, 4));
    }

    [TestMethod]
    public void Exceptions()
    {
        // bad lookback period
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            Indicator.GetWma(quotes, 0));
    }
}
