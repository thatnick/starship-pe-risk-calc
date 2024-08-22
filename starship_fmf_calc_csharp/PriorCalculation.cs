using Microsoft.Extensions.Logging;

public static class PriorCalculator
{
    public static double CalcPriorMean(PriorCoeffs coeffs, PriorForm inputs)
    {
        double priorMean = coeffs.Constant;

        priorMean += coeffs.Age * inputs.Age;
        priorMean += coeffs.Height * inputs.Height;
        priorMean += coeffs.Ethnicity * 0;
        priorMean += coeffs.ChronicHypert * 0 ;
        priorMean += coeffs.Autoimmune * 0 ;
        priorMean += coeffs.IVF * 0;
        priorMean += coeffs.ParousWithPE * 0;
        priorMean += coeffs.ParousWithPEGestAge * 0;
        priorMean += coeffs.ParousNoPE * 0;
        priorMean += coeffs.ParousNoPEInterval * 0;
        priorMean += coeffs.ParousNoPEIntervalSquared * 0;
        priorMean += coeffs.ParousNoPEGestationalAge * 0;
        priorMean += coeffs.Weight * inputs.Weight;
        priorMean += coeffs.FamilyHistoryPE * 0;
        priorMean += coeffs.Diabetes * 0;

        Console.WriteLine($"priorMean: {priorMean}");

        return priorMean;
    }
}
