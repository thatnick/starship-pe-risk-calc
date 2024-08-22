using Accord.Math.Integration;
using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Math;

public static class RiskCalculator
{
    public static (double, double) Calculate(
        double gestationalAge,
        double targetGestationalAge,
        double priorMean,
        double priorSD,
        double[] moMs,
        double[,] sigma)
    {
        Console.WriteLine($"currentGestAge: {gestationalAge}, targetGestAge: {targetGestationalAge}, priorMean: {priorMean}, priorSD: {priorSD}");
        Console.WriteLine($"MoMs: {string.Join(", ", moMs)}");
        Console.WriteLine($"sigma: {string.Join(", ", sigma.Cast<double>())}");

        var normalDistribution = new NormalDistribution(priorMean, priorSD);

        double funcPrior(double gestAge) => normalDistribution.ProbabilityDensityFunction(gestAge);
        double funcPost(double gestAge)
        {
            var multivariateNormalDistribution = new MultivariateNormalDistribution(CalculateMeanVector(gestAge), sigma);
            return normalDistribution.ProbabilityDensityFunction(gestAge) * multivariateNormalDistribution.ProbabilityDensityFunction(moMs);
        }

        var numeratorPrior = NonAdaptiveGaussKronrod.Integrate(
        funcPrior,
            gestationalAge,
            targetGestationalAge
        );
        var denominatorPrior = InfiniteAdaptiveGaussKronrod.Integrate(
            funcPrior,
            gestationalAge,
            double.PositiveInfinity);

        var numeratorPost = NonAdaptiveGaussKronrod.Integrate(
        funcPost,
            gestationalAge,
            targetGestationalAge
        );
        var denominatorPost = InfiniteAdaptiveGaussKronrod.Integrate(
            funcPost,
            gestationalAge,
            double.PositiveInfinity);

        return (numeratorPrior / denominatorPrior, numeratorPost / denominatorPost);
    }

    private static double[] CalculateMeanVector(double gestationalAge)
    {
        // Coefficients for the markers at 12 weeks
        var markerCoefficients = new List<Tuple<double, double>>
    {
        Tuple.Create(0.088997, -0.0016711), // MAP at 12 weeks
        Tuple.Create(0.5861, -0.014233),    // UtA-PI at 12 weeks
        Tuple.Create(-0.92352, 0.021584)    // PLGF at 12 weeks
    };
        return markerCoefficients.Select(marker =>
        {
            var (b0, b1) = marker;
            var threshold = -b0 / b1;
            return gestationalAge < threshold ? b0 + b1 * gestationalAge : 0;
        }).ToArray();
    }
}
