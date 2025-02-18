using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skender.Stock.Indicators;

namespace Internal.Tests;

[TestClass]
public class TripleEma : TestBase
{
    [TestMethod]
    public void Standard()
    {
        List<TemaResult> results = quotes.GetTripleEma(20).ToList();

        // assertions

        // proper quantities
        // should always be the same number of results as there is quotes
        Assert.AreEqual(502, results.Count);
        Assert.AreEqual(445, results.Where(x => x.Tema != null).Count());

        // sample values
        TemaResult r1 = results[67];
        Assert.AreEqual(222.9105m, Math.Round((decimal)r1.Tema, 4));

        TemaResult r2 = results[249];
        Assert.AreEqual(258.6208m, Math.Round((decimal)r2.Tema, 4));

        TemaResult r3 = results[501];
        Assert.AreEqual(238.7690m, Math.Round((decimal)r3.Tema, 4));
    }

    [TestMethod]
    public void BadData()
    {
        IEnumerable<TemaResult> r = Indicator.GetTripleEma(badQuotes, 15);
        Assert.AreEqual(502, r.Count());
    }

    [TestMethod]
    public void NoQuotes()
    {
        IEnumerable<TemaResult> r0 = noquotes.GetTripleEma(5);
        Assert.AreEqual(0, r0.Count());

        IEnumerable<TemaResult> r1 = onequote.GetTripleEma(5);
        Assert.AreEqual(1, r1.Count());
    }

    [TestMethod]
    public void Removed()
    {
        List<TemaResult> results = quotes.GetTripleEma(20)
            .RemoveWarmupPeriods()
            .ToList();

        // assertions
        Assert.AreEqual(502 - ((3 * 20) + 100), results.Count);

        TemaResult last = results.LastOrDefault();
        Assert.AreEqual(238.7690m, Math.Round((decimal)last.Tema, 4));
    }

    [TestMethod]
    public void Exceptions()
    {
        // bad lookback period
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            Indicator.GetTripleEma(quotes, 0));
    }
}
