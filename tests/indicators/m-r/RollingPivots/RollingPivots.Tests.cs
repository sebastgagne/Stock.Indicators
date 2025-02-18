using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skender.Stock.Indicators;

namespace Internal.Tests;

[TestClass]
public class RollingPivots : TestBase
{
    [TestMethod]
    public void Standard()
    {
        int windowPeriods = 11;
        int offsetPeriods = 9;
        PivotPointType pointType = PivotPointType.Standard;

        List<RollingPivotsResult> results =
            quotes.GetRollingPivots(windowPeriods, offsetPeriods, pointType)
            .ToList();

        // assertions

        // proper quantities
        // should always be the same number of results as there is quotes
        Assert.AreEqual(502, results.Count);
        Assert.AreEqual(482, results.Where(x => x.PP != null).Count());

        // sample values
        RollingPivotsResult r1 = results[19];
        Assert.AreEqual(null, r1.PP);
        Assert.AreEqual(null, r1.S1);
        Assert.AreEqual(null, r1.S2);
        Assert.AreEqual(null, r1.S3);
        Assert.AreEqual(null, r1.S4);
        Assert.AreEqual(null, r1.R1);
        Assert.AreEqual(null, r1.R2);
        Assert.AreEqual(null, r1.R3);
        Assert.AreEqual(null, r1.R4);

        RollingPivotsResult r2 = results[20];
        Assert.AreEqual(213.6367m, Math.Round((decimal)r2.PP, 4));
        Assert.AreEqual(212.1033m, Math.Round((decimal)r2.S1, 4));
        Assert.AreEqual(209.9867m, Math.Round((decimal)r2.S2, 4));
        Assert.AreEqual(208.4533m, Math.Round((decimal)r2.S3, 4));
        Assert.AreEqual(null, r2.S4);
        Assert.AreEqual(215.7533m, Math.Round((decimal)r2.R1, 4));
        Assert.AreEqual(217.2867m, Math.Round((decimal)r2.R2, 4));
        Assert.AreEqual(219.4033m, Math.Round((decimal)r2.R3, 4));
        Assert.AreEqual(null, r2.R4);

        RollingPivotsResult r3 = results[149];
        Assert.AreEqual(233.6333m, Math.Round((decimal)r3.PP, 4));
        Assert.AreEqual(231.3567m, Math.Round((decimal)r3.S1, 4));
        Assert.AreEqual(227.3733m, Math.Round((decimal)r3.S2, 4));
        Assert.AreEqual(225.0967m, Math.Round((decimal)r3.S3, 4));
        Assert.AreEqual(null, r3.S4);
        Assert.AreEqual(237.6167m, Math.Round((decimal)r3.R1, 4));
        Assert.AreEqual(239.8933m, Math.Round((decimal)r3.R2, 4));
        Assert.AreEqual(243.8767m, Math.Round((decimal)r3.R3, 4));
        Assert.AreEqual(null, r3.R4);

        RollingPivotsResult r4 = results[249];
        Assert.AreEqual(253.9533m, Math.Round((decimal)r4.PP, 4));
        Assert.AreEqual(251.5267m, Math.Round((decimal)r4.S1, 4));
        Assert.AreEqual(247.4433m, Math.Round((decimal)r4.S2, 4));
        Assert.AreEqual(245.0167m, Math.Round((decimal)r4.S3, 4));
        Assert.AreEqual(null, r4.S4);
        Assert.AreEqual(258.0367m, Math.Round((decimal)r4.R1, 4));
        Assert.AreEqual(260.4633m, Math.Round((decimal)r4.R2, 4));
        Assert.AreEqual(264.5467m, Math.Round((decimal)r4.R3, 4));
        Assert.AreEqual(null, r4.R4);

        RollingPivotsResult r5 = results[501];
        Assert.AreEqual(260.0267m, Math.Round((decimal)r5.PP, 4));
        Assert.AreEqual(246.4633m, Math.Round((decimal)r5.S1, 4));
        Assert.AreEqual(238.7767m, Math.Round((decimal)r5.S2, 4));
        Assert.AreEqual(225.2133m, Math.Round((decimal)r5.S3, 4));
        Assert.AreEqual(null, r5.S4);
        Assert.AreEqual(267.7133m, Math.Round((decimal)r5.R1, 4));
        Assert.AreEqual(281.2767m, Math.Round((decimal)r5.R2, 4));
        Assert.AreEqual(288.9633m, Math.Round((decimal)r5.R3, 4));
        Assert.AreEqual(null, r5.R4);
    }

    [TestMethod]
    public void Camarilla()
    {
        int windowPeriods = 10;
        int offsetPeriods = 0;
        PivotPointType pointType = PivotPointType.Camarilla;

        IEnumerable<Quote> h = TestData.GetDefault(38);
        List<RollingPivotsResult> results =
            Indicator.GetRollingPivots(h, windowPeriods, offsetPeriods, pointType)
            .ToList();

        // assertions

        // proper quantities
        // should always be the same number of results as there is quotes
        Assert.AreEqual(38, results.Count);
        Assert.AreEqual(28, results.Where(x => x.PP != null).Count());

        // sample values
        RollingPivotsResult r1 = results[9];
        Assert.AreEqual(null, r1.R4);
        Assert.AreEqual(null, r1.R3);
        Assert.AreEqual(null, r1.PP);
        Assert.AreEqual(null, r1.S1);
        Assert.AreEqual(null, r1.S2);
        Assert.AreEqual(null, r1.R1);
        Assert.AreEqual(null, r1.R2);
        Assert.AreEqual(null, r1.S3);
        Assert.AreEqual(null, r1.S4);

        RollingPivotsResult r2 = results[10];
        Assert.AreEqual(267.0800m, Math.Round((decimal)r2.PP, 4));
        Assert.AreEqual(265.8095m, Math.Round((decimal)r2.S1, 4));
        Assert.AreEqual(264.5390m, Math.Round((decimal)r2.S2, 4));
        Assert.AreEqual(263.2685m, Math.Round((decimal)r2.S3, 4));
        Assert.AreEqual(259.4570m, Math.Round((decimal)r2.S4, 4));
        Assert.AreEqual(268.3505m, Math.Round((decimal)r2.R1, 4));
        Assert.AreEqual(269.6210m, Math.Round((decimal)r2.R2, 4));
        Assert.AreEqual(270.8915m, Math.Round((decimal)r2.R3, 4));
        Assert.AreEqual(274.7030m, Math.Round((decimal)r2.R4, 4));

        RollingPivotsResult r3 = results[22];
        Assert.AreEqual(263.2900m, Math.Round((decimal)r3.PP, 4));
        Assert.AreEqual(261.6840m, Math.Round((decimal)r3.S1, 4));
        Assert.AreEqual(260.0780m, Math.Round((decimal)r3.S2, 4));
        Assert.AreEqual(258.4720m, Math.Round((decimal)r3.S3, 4));
        Assert.AreEqual(253.6540m, Math.Round((decimal)r3.S4, 4));
        Assert.AreEqual(264.8960m, Math.Round((decimal)r3.R1, 4));
        Assert.AreEqual(266.5020m, Math.Round((decimal)r3.R2, 4));
        Assert.AreEqual(268.1080m, Math.Round((decimal)r3.R3, 4));
        Assert.AreEqual(272.9260m, Math.Round((decimal)r3.R4, 4));

        RollingPivotsResult r4 = results[23];
        Assert.AreEqual(257.1700m, Math.Round((decimal)r4.PP, 4));
        Assert.AreEqual(255.5640m, Math.Round((decimal)r4.S1, 4));
        Assert.AreEqual(253.9580m, Math.Round((decimal)r4.S2, 4));
        Assert.AreEqual(252.3520m, Math.Round((decimal)r4.S3, 4));
        Assert.AreEqual(247.5340m, Math.Round((decimal)r4.S4, 4));
        Assert.AreEqual(258.7760m, Math.Round((decimal)r4.R1, 4));
        Assert.AreEqual(260.3820m, Math.Round((decimal)r4.R2, 4));
        Assert.AreEqual(261.9880m, Math.Round((decimal)r4.R3, 4));
        Assert.AreEqual(266.8060m, Math.Round((decimal)r4.R4, 4));

        RollingPivotsResult r5 = results[37];
        Assert.AreEqual(243.1500m, Math.Round((decimal)r5.PP, 4));
        Assert.AreEqual(240.5650m, Math.Round((decimal)r5.S1, 4));
        Assert.AreEqual(237.9800m, Math.Round((decimal)r5.S2, 4));
        Assert.AreEqual(235.3950m, Math.Round((decimal)r5.S3, 4));
        Assert.AreEqual(227.6400m, Math.Round((decimal)r5.S4, 4));
        Assert.AreEqual(245.7350m, Math.Round((decimal)r5.R1, 4));
        Assert.AreEqual(248.3200m, Math.Round((decimal)r5.R2, 4));
        Assert.AreEqual(250.9050m, Math.Round((decimal)r5.R3, 4));
        Assert.AreEqual(258.6600m, Math.Round((decimal)r5.R4, 4));
    }

    [TestMethod]
    public void Demark()
    {
        int windowPeriods = 10;
        int offsetPeriods = 10;
        PivotPointType pointType = PivotPointType.Demark;

        List<RollingPivotsResult> results =
            Indicator.GetRollingPivots(quotes, windowPeriods, offsetPeriods, pointType)
            .ToList();

        // assertions

        // proper quantities
        // should always be the same number of results as there is quotes
        Assert.AreEqual(502, results.Count);
        Assert.AreEqual(482, results.Where(x => x.PP != null).Count());

        // sample values
        RollingPivotsResult r1 = results[19];
        Assert.AreEqual(null, r1.PP);
        Assert.AreEqual(null, r1.S1);
        Assert.AreEqual(null, r1.S2);
        Assert.AreEqual(null, r1.S3);
        Assert.AreEqual(null, r1.S4);
        Assert.AreEqual(null, r1.R1);
        Assert.AreEqual(null, r1.R2);
        Assert.AreEqual(null, r1.R3);
        Assert.AreEqual(null, r1.R4);

        RollingPivotsResult r2 = results[20];
        Assert.AreEqual(212.9900m, Math.Round((decimal)r2.PP, 4));
        Assert.AreEqual(210.8100m, Math.Round((decimal)r2.S1, 4));
        Assert.AreEqual(214.4600m, Math.Round((decimal)r2.R1, 4));
        Assert.AreEqual(null, r2.R2);
        Assert.AreEqual(null, r2.R3);
        Assert.AreEqual(null, r2.R4);
        Assert.AreEqual(null, r2.S2);
        Assert.AreEqual(null, r2.S3);
        Assert.AreEqual(null, r2.S4);

        RollingPivotsResult r3 = results[149];
        Assert.AreEqual(232.6525m, Math.Round((decimal)r3.PP, 4));
        Assert.AreEqual(229.3950m, Math.Round((decimal)r3.S1, 4));
        Assert.AreEqual(235.6550m, Math.Round((decimal)r3.R1, 4));
        Assert.AreEqual(null, r3.R2);
        Assert.AreEqual(null, r3.R3);
        Assert.AreEqual(null, r3.R4);
        Assert.AreEqual(null, r3.S2);
        Assert.AreEqual(null, r3.S3);
        Assert.AreEqual(null, r3.S4);

        RollingPivotsResult r4 = results[250];
        Assert.AreEqual(252.9325m, Math.Round((decimal)r4.PP, 4));
        Assert.AreEqual(249.4850m, Math.Round((decimal)r4.S1, 4));
        Assert.AreEqual(255.9950m, Math.Round((decimal)r4.R1, 4));
        Assert.AreEqual(null, r4.R2);
        Assert.AreEqual(null, r4.R3);
        Assert.AreEqual(null, r4.R4);
        Assert.AreEqual(null, r4.S2);
        Assert.AreEqual(null, r4.S3);
        Assert.AreEqual(null, r4.S4);

        RollingPivotsResult r5 = results[251];
        Assert.AreEqual(252.6700m, Math.Round((decimal)r5.PP, 4));
        Assert.AreEqual(248.9600m, Math.Round((decimal)r5.S1, 4));
        Assert.AreEqual(255.4700m, Math.Round((decimal)r5.R1, 4));
        Assert.AreEqual(null, r5.R2);
        Assert.AreEqual(null, r5.R3);
        Assert.AreEqual(null, r5.R4);
        Assert.AreEqual(null, r5.S2);
        Assert.AreEqual(null, r5.S3);
        Assert.AreEqual(null, r5.S4);

        RollingPivotsResult r6 = results[501];
        Assert.AreEqual(264.6125m, Math.Round((decimal)r6.PP, 4));
        Assert.AreEqual(255.6350m, Math.Round((decimal)r6.S1, 4));
        Assert.AreEqual(276.8850m, Math.Round((decimal)r6.R1, 4));
        Assert.AreEqual(null, r6.R2);
        Assert.AreEqual(null, r6.R3);
        Assert.AreEqual(null, r6.R4);
        Assert.AreEqual(null, r6.S2);
        Assert.AreEqual(null, r6.S3);
        Assert.AreEqual(null, r6.S4);
    }

    [TestMethod]
    public void Fibonacci()
    {
        int windowPeriods = 44;
        int offsetPeriods = 15;
        PivotPointType pointType = PivotPointType.Fibonacci;

        IEnumerable<Quote> h = TestData.GetIntraday(300);
        List<RollingPivotsResult> results =
            Indicator.GetRollingPivots(h, windowPeriods, offsetPeriods, pointType)
            .ToList();

        // assertions

        // proper quantities
        // should always be the same number of results as there is quotes
        Assert.AreEqual(300, results.Count);
        Assert.AreEqual(241, results.Where(x => x.PP != null).Count());

        // sample values
        RollingPivotsResult r1 = results[58];
        Assert.AreEqual(null, r1.R4);
        Assert.AreEqual(null, r1.R3);
        Assert.AreEqual(null, r1.PP);
        Assert.AreEqual(null, r1.S1);
        Assert.AreEqual(null, r1.S2);
        Assert.AreEqual(null, r1.R1);
        Assert.AreEqual(null, r1.R2);
        Assert.AreEqual(null, r1.S3);
        Assert.AreEqual(null, r1.S4);

        RollingPivotsResult r2 = results[59];
        Assert.AreEqual(368.4283m, Math.Round((decimal)r2.PP, 4));
        Assert.AreEqual(367.8553m, Math.Round((decimal)r2.S1, 4));
        Assert.AreEqual(367.5013m, Math.Round((decimal)r2.S2, 4));
        Assert.AreEqual(366.9283m, Math.Round((decimal)r2.S3, 4));
        Assert.AreEqual(369.0013m, Math.Round((decimal)r2.R1, 4));
        Assert.AreEqual(369.3553m, Math.Round((decimal)r2.R2, 4));
        Assert.AreEqual(369.9283m, Math.Round((decimal)r2.R3, 4));

        RollingPivotsResult r3 = results[118];
        Assert.AreEqual(369.1573m, Math.Round((decimal)r3.PP, 4));
        Assert.AreEqual(368.7333m, Math.Round((decimal)r3.S1, 4));
        Assert.AreEqual(368.4713m, Math.Round((decimal)r3.S2, 4));
        Assert.AreEqual(368.0473m, Math.Round((decimal)r3.S3, 4));
        Assert.AreEqual(369.5813m, Math.Round((decimal)r3.R1, 4));
        Assert.AreEqual(369.8433m, Math.Round((decimal)r3.R2, 4));
        Assert.AreEqual(370.2673m, Math.Round((decimal)r3.R3, 4));

        RollingPivotsResult r4 = results[119];
        Assert.AreEqual(369.1533m, Math.Round((decimal)r4.PP, 4));
        Assert.AreEqual(368.7293m, Math.Round((decimal)r4.S1, 4));
        Assert.AreEqual(368.4674m, Math.Round((decimal)r4.S2, 4));
        Assert.AreEqual(368.0433m, Math.Round((decimal)r4.S3, 4));
        Assert.AreEqual(369.5774m, Math.Round((decimal)r4.R1, 4));
        Assert.AreEqual(369.8393m, Math.Round((decimal)r4.R2, 4));
        Assert.AreEqual(370.2633m, Math.Round((decimal)r4.R3, 4));

        RollingPivotsResult r5 = results[149];
        Assert.AreEqual(369.0183m, Math.Round((decimal)r5.PP, 4));
        Assert.AreEqual(368.6593m, Math.Round((decimal)r5.S1, 4));
        Assert.AreEqual(368.4374m, Math.Round((decimal)r5.S2, 4));
        Assert.AreEqual(368.0783m, Math.Round((decimal)r5.S3, 4));
        Assert.AreEqual(369.3774m, Math.Round((decimal)r5.R1, 4));
        Assert.AreEqual(369.5993m, Math.Round((decimal)r5.R2, 4));
        Assert.AreEqual(369.9583m, Math.Round((decimal)r5.R3, 4));

        RollingPivotsResult r6 = results[299];
        Assert.AreEqual(367.7567m, Math.Round((decimal)r6.PP, 4));
        Assert.AreEqual(367.3174m, Math.Round((decimal)r6.S1, 4));
        Assert.AreEqual(367.0460m, Math.Round((decimal)r6.S2, 4));
        Assert.AreEqual(366.6067m, Math.Round((decimal)r6.S3, 4));
        Assert.AreEqual(368.1960m, Math.Round((decimal)r6.R1, 4));
        Assert.AreEqual(368.4674m, Math.Round((decimal)r6.R2, 4));
        Assert.AreEqual(368.9067m, Math.Round((decimal)r6.R3, 4));
    }

    [TestMethod]
    public void Woodie()
    {
        int windowPeriods = 375;
        int offsetPeriods = 16;
        PivotPointType pointType = PivotPointType.Woodie;

        IEnumerable<Quote> h = TestData.GetIntraday(1564);
        List<RollingPivotsResult> results =
            Indicator.GetRollingPivots(h, windowPeriods, offsetPeriods, pointType)
            .ToList();

        // assertions

        // proper quantities
        // should always be the same number of results as there is quotes
        Assert.AreEqual(1564, results.Count);
        Assert.AreEqual(1173, results.Where(x => x.PP != null).Count());

        // sample values
        RollingPivotsResult r2 = results[390];
        Assert.AreEqual(null, r2.R4);
        Assert.AreEqual(null, r2.R3);
        Assert.AreEqual(null, r2.PP);
        Assert.AreEqual(null, r2.S1);
        Assert.AreEqual(null, r2.S2);
        Assert.AreEqual(null, r2.R1);
        Assert.AreEqual(null, r2.R2);
        Assert.AreEqual(null, r2.S3);
        Assert.AreEqual(null, r2.S4);

        RollingPivotsResult r3 = results[391];
        Assert.AreEqual(368.7850m, Math.Round((decimal)r3.PP, 4));
        Assert.AreEqual(367.9901m, Math.Round((decimal)r3.S1, 4));
        Assert.AreEqual(365.1252m, Math.Round((decimal)r3.S2, 4));
        Assert.AreEqual(364.3303m, Math.Round((decimal)r3.S3, 4));
        Assert.AreEqual(371.6499m, Math.Round((decimal)r3.R1, 4));
        Assert.AreEqual(372.4448m, Math.Round((decimal)r3.R2, 4));
        Assert.AreEqual(375.3097m, Math.Round((decimal)r3.R3, 4));

        RollingPivotsResult r4 = results[1172];
        Assert.AreEqual(371.75m, Math.Round((decimal)r4.PP, 4));
        Assert.AreEqual(371.04m, Math.Round((decimal)r4.S1, 4));
        Assert.AreEqual(369.35m, Math.Round((decimal)r4.S2, 4));
        Assert.AreEqual(368.64m, Math.Round((decimal)r4.S3, 4));
        Assert.AreEqual(373.44m, Math.Round((decimal)r4.R1, 4));
        Assert.AreEqual(374.15m, Math.Round((decimal)r4.R2, 4));
        Assert.AreEqual(375.84m, Math.Round((decimal)r4.R3, 4));

        RollingPivotsResult r5 = results[1173];
        Assert.AreEqual(371.3625m, Math.Round((decimal)r5.PP, 4));
        Assert.AreEqual(370.2650m, Math.Round((decimal)r5.S1, 4));
        Assert.AreEqual(369.9525m, Math.Round((decimal)r5.S2, 4));
        Assert.AreEqual(368.8550m, Math.Round((decimal)r5.S3, 4));
        Assert.AreEqual(371.6750m, Math.Round((decimal)r5.R1, 4));
        Assert.AreEqual(372.7725m, Math.Round((decimal)r5.R2, 4));
        Assert.AreEqual(373.0850m, Math.Round((decimal)r5.R3, 4));

        RollingPivotsResult r6 = results[1563];
        Assert.AreEqual(369.38m, Math.Round((decimal)r6.PP, 4));
        Assert.AreEqual(366.52m, Math.Round((decimal)r6.S1, 4));
        Assert.AreEqual(364.16m, Math.Round((decimal)r6.S2, 4));
        Assert.AreEqual(361.30m, Math.Round((decimal)r6.S3, 4));
        Assert.AreEqual(371.74m, Math.Round((decimal)r6.R1, 4));
        Assert.AreEqual(374.60m, Math.Round((decimal)r6.R2, 4));
        Assert.AreEqual(376.96m, Math.Round((decimal)r6.R3, 4));
    }

    [TestMethod]
    public void BadData()
    {
        IEnumerable<RollingPivotsResult> r = Indicator.GetRollingPivots(badQuotes, 5, 5);
        Assert.AreEqual(502, r.Count());
    }

    [TestMethod]
    public void NoQuotes()
    {
        IEnumerable<RollingPivotsResult> r0 = noquotes.GetRollingPivots(5, 2);
        Assert.AreEqual(0, r0.Count());

        IEnumerable<RollingPivotsResult> r1 = onequote.GetRollingPivots(5, 2);
        Assert.AreEqual(1, r1.Count());
    }

    [TestMethod]
    public void Removed()
    {
        int windowPeriods = 11;
        int offsetPeriods = 9;
        PivotPointType pointType = PivotPointType.Standard;

        List<RollingPivotsResult> results =
            quotes.GetRollingPivots(windowPeriods, offsetPeriods, pointType)
                .RemoveWarmupPeriods()
                .ToList();

        // assertions
        Assert.AreEqual(502 - (windowPeriods + offsetPeriods), results.Count);

        RollingPivotsResult last = results.LastOrDefault();
        Assert.AreEqual(260.0267m, Math.Round((decimal)last.PP, 4));
        Assert.AreEqual(246.4633m, Math.Round((decimal)last.S1, 4));
        Assert.AreEqual(238.7767m, Math.Round((decimal)last.S2, 4));
        Assert.AreEqual(225.2133m, Math.Round((decimal)last.S3, 4));
        Assert.AreEqual(null, last.S4);
        Assert.AreEqual(267.7133m, Math.Round((decimal)last.R1, 4));
        Assert.AreEqual(281.2767m, Math.Round((decimal)last.R2, 4));
        Assert.AreEqual(288.9633m, Math.Round((decimal)last.R3, 4));
        Assert.AreEqual(null, last.R4);
    }

    [TestMethod]
    public void Exceptions()
    {
        // bad window period
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            Indicator.GetRollingPivots(quotes, 0, 10));

        // bad offset period
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            Indicator.GetRollingPivots(quotes, 10, -1));
    }
}
