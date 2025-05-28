namespace PeRiskCalc.Api;

public static class PriorCalculator
{
    public static double CalcPriorMean(PriorCoeffs coeffs, PriorInputs inputs)
    {
        double priorMean = coeffs.Constant;

        priorMean += coeffs.Age * inputs.Age;
        priorMean += coeffs.Height * inputs.Height;
        priorMean += coeffs.Ethnicity;
        priorMean += coeffs.ChronicHypert;
        priorMean += coeffs.SleOrAps;
        priorMean += coeffs.IVF;
        priorMean += coeffs.Weight * inputs.Weight;
        priorMean += coeffs.MotherPe;
        priorMean += coeffs.Diabetes;
        priorMean += coeffs.ParousWithPE;
        priorMean += coeffs.ParousWithPELastGestAge * inputs.ParousLastGestAgeWeeks;
        priorMean += coeffs.ParousNoPEIntercept;
        priorMean += coeffs.ParousPregIntervalToPowCalc1 * inputs.ParousPregIntervalToPowCalc1;
        priorMean += coeffs.ParousPregIntervalToPowCalc2 * inputs.ParousPregIntervalToPowCalc2;
        priorMean += coeffs.ParousNoPELastGestAge * inputs.ParousLastGestAgeWeeks;
        priorMean -= coeffs.TwinFactor;
        
        return priorMean;
    }
}