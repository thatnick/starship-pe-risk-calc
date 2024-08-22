using Accord.Math.Integration;
using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Math;

public static class RiskCalculator
{
    public static double CalcPostRisk(
        double currentGestAge,
        double targetGestAge,
        double priorMean,
        double priorSD,
        double[] MoMs,
        double[,] sigma)
    {
        var meanVector = CalcMeanVector(currentGestAge);
        var normalDistribution = new NormalDistribution(priorMean, priorSD);
        //var multivariateNormalDistribution = new MultivariateNormalDistribution(meanVector, sigma);
        //Func<double, double> h = gestAge => normalDistribution.ProbabilityDensityFunction(gestAge) * multivariateNormalDistribution.ProbabilityDensityFunction(CalcMeanVector(gestAge));
        double h(double gestAge) => normalDistribution.ProbabilityDensityFunction(gestAge);

        var numerator = NonAdaptiveGaussKronrod.Integrate(
            h,
            currentGestAge,
            targetGestAge
        );

        var denominator = InfiniteAdaptiveGaussKronrod.Integrate(h,
            currentGestAge,
            double.PositiveInfinity);

        return numerator / denominator;
    }

    private static double[] CalcMeanVector(double gestAge)
    {
        // Coefficients for the markers at 12 weeks
        var markers = new List<Tuple<double, double>>
    {
        Tuple.Create(0.088997, -0.0016711), // MAP at 12 weeks
        Tuple.Create(0.5861, -0.014233),    // UtA-PI at 12 weeks
        Tuple.Create(-0.92352, 0.021584)    // PLGF at 12 weeks
    };
        var mv = markers.Select(marker =>
        {
            var (b0, b1) = marker;
            var threshold = -b0 / b1;
            return gestAge < threshold ? b0 + b1 * gestAge : 0;
        }).ToArray();
        Console.WriteLine("Calc mean vector");
        foreach (var item in mv)
        {
            Console.WriteLine(item.ToString());
        }

        return mv;
    }

}
