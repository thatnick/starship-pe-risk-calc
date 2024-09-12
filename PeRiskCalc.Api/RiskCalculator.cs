using Accord.Math.Integration;
using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Math;

public static class RiskCalculator
{
    public static (double, double) Calculate(
        double gestAge,
        double targetGestAge,
        double priorMean,
        double priorSD,
        double[] momVector,
        double[,] sigma,
        bool isMapAvailable, bool isUtaPiAvailable, bool isPlgfAvailable)
    {
        System.Console.WriteLine("******");
        System.Console.WriteLine("st dev is " + priorSD);
        System.Console.WriteLine("******");

        // Adjust the MoM vector and sigma matrix based on availability
        var (adjustedMoMs, adjustedSigma) = AdjustForMissingMarkers(momVector, sigma, isMapAvailable, isUtaPiAvailable, isPlgfAvailable);

        var normalDistribution = new NormalDistribution(priorMean, priorSD);

        double funcPrior(double gestAge) => normalDistribution.ProbabilityDensityFunction(gestAge);
        double funcPost(double gestAge)
        {
            var multivariateNormalDistribution = new MultivariateNormalDistribution(CalculateMeanVector(gestAge, isMapAvailable, isUtaPiAvailable, isPlgfAvailable), adjustedSigma);
            return normalDistribution.ProbabilityDensityFunction(gestAge) * multivariateNormalDistribution.ProbabilityDensityFunction(adjustedMoMs);
        }

        // Perform risk calculation as before
        var numeratorPrior = NonAdaptiveGaussKronrod.Integrate(
            funcPrior,
            gestAge,
            targetGestAge
        );
        var denominatorPrior = InfiniteAdaptiveGaussKronrod.Integrate(
            funcPrior,
            gestAge,
            double.PositiveInfinity);

        var numeratorPost = NonAdaptiveGaussKronrod.Integrate(
            funcPost,
            gestAge,
            targetGestAge
        );
        var denominatorPost = InfiniteAdaptiveGaussKronrod.Integrate(
            funcPost,
            gestAge,
            double.PositiveInfinity);

        return (numeratorPrior / denominatorPrior, numeratorPost / denominatorPost);
    }

    // Adjust the MoM vector and Sigma matrix
    private static (double[] adjustedMoMs, double[,] adjustedSigma) AdjustForMissingMarkers(
        double[] moMs, double[,] sigma, bool isMapAvailable, bool isUtaPiAvailable, bool isPlgfAvailable)
    {
        // Build list of available indices and filter out missing markers
        var availableIndices = new List<int>();
        if (isMapAvailable) availableIndices.Add(0);
        if (isUtaPiAvailable) availableIndices.Add(1);
        if (isPlgfAvailable) availableIndices.Add(2);

        // Adjust the MoM vector based on availability
        var adjustedMoMs = availableIndices.Select(i => moMs[i]).ToArray();

        // Adjust the Sigma matrix by selecting the relevant rows and columns
        int size = availableIndices.Count;
        var adjustedSigma = new double[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                adjustedSigma[i, j] = sigma[availableIndices[i], availableIndices[j]];
            }
        }

        return (adjustedMoMs, adjustedSigma);
    }

    // Adjust the mean vector based on marker availability
    private static double[] CalculateMeanVector(double gestationalAge, bool isMapAvailable, bool isUtaPiAvailable, bool isPlgfAvailable)
    {
        var markerCoefficients = new List<Tuple<double, double>>
    {
        Tuple.Create(0.088997, -0.0016711), // MAP at 12 weeks
        Tuple.Create(0.5861, -0.014233),    // UtA-PI at 12 weeks
        Tuple.Create(-0.92352, 0.021584)    // PLGF at 12 weeks
    };

        var availableCoefficients = new List<Tuple<double, double>>();
        if (isMapAvailable) availableCoefficients.Add(markerCoefficients[0]);
        if (isUtaPiAvailable) availableCoefficients.Add(markerCoefficients[1]);
        if (isPlgfAvailable) availableCoefficients.Add(markerCoefficients[2]);

        return availableCoefficients.Select(marker =>
        {
            var (b0, b1) = marker;
            var threshold = -b0 / b1;
            return gestationalAge < threshold ? b0 + b1 * gestationalAge : 0;
        }).ToArray();
    }

}
