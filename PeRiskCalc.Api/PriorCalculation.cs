namespace PeRiskCalc.Api;

public static class PriorCalculator
{
    public static double CalcPriorMean(PriorCoeffs coeffs, PriorInputs inputs)
    {
        double priorMean = coeffs.Constant;
        Console.WriteLine("******");
        Console.WriteLine($"STARTING priorMean: {priorMean}");
        Console.WriteLine($"Coeffs constant: {coeffs.Constant}");

        priorMean += coeffs.Age * inputs.Age;
        // Console.WriteLine($"Prior mean after age: {priorMean}");
        // Console.WriteLine($"Coeffs age calc: {coeffs.Age * inputs.Age}");

        priorMean += coeffs.Height * inputs.Height;
        // Console.WriteLine($"Prior mean after height: {priorMean}");
        // Console.WriteLine($"Coeffs height  calc: {coeffs.Height * inputs.Height}");

        priorMean += coeffs.Ethnicity;
        // Console.WriteLine($"Prior mean after Ethnicity: {priorMean}");
        // Console.WriteLine($"Coeffs ethnicity  calc: {coeffs.Ethnicity}");

        priorMean += coeffs.ChronicHypert;
        // Console.WriteLine($"Prior mean after ChronicHypert: {priorMean}");
        // Console.WriteLine($"Coeffs ChronicHypert  calc: {coeffs.ChronicHypert}");

        priorMean += coeffs.SleOrAps;
        // Console.WriteLine($"Prior mean after SleOrAps: {priorMean}");
        // Console.WriteLine($"Coeffs SleOrAps  calc: {coeffs.SleOrAps}");

        priorMean += coeffs.IVF;
        // Console.WriteLine($"Prior mean after ivf: {priorMean}");
        // Console.WriteLine($"Coeffs IVF  calc: {coeffs.IVF}");

        priorMean += coeffs.Weight * inputs.Weight;
        // Console.WriteLine($"Prior mean after weight: {priorMean}");
        // Console.WriteLine($"Coeffs Weight calc: {coeffs.Weight * inputs.Weight}");

        priorMean += coeffs.MotherPe;
        // Console.WriteLine($"Prior mean after mother pe: {priorMean}");
        // Console.WriteLine($"Coeffs mother pe calc: {coeffs.MotherPe}");

        priorMean += coeffs.Diabetes;
        // Console.WriteLine($"Prior mean after diabetes: {priorMean}"); 
        // Console.WriteLine($"Coeffs diabetes calc: {coeffs.Diabetes}");

        Console.WriteLine($"Prior mean before parity: {priorMean}");

        priorMean += coeffs.ParousWithPE;
        Console.WriteLine($"Prior mean after ParousWithPE: {priorMean}");
        Console.WriteLine($"Coeffs ParousWithPE calc: {coeffs.ParousWithPE}");

        priorMean += coeffs.ParousWithPELastGestAge * inputs.ParousLastGestAgeWeeks;
        Console.WriteLine($"Prior mean after ParousWithPELastGestAge {priorMean}");
        Console.WriteLine($"Input ParousLastGestAgeWeeks {inputs.ParousLastGestAgeWeeks}");
        Console.WriteLine($"Coeffs ParousWithPELastGestAge calc: {coeffs.ParousWithPELastGestAge * inputs.ParousLastGestAgeWeeks}");

        priorMean += coeffs.ParousNoPEIntercept;
        Console.WriteLine($"Prior mean after parnopeinter: {priorMean}");
        Console.WriteLine($"Coeffs ParousNoPEInterval calc: {coeffs.ParousNoPEIntercept}");

        Console.WriteLine($"ParousPregInterval: {inputs.ParousPregInterval}");

        priorMean += coeffs.ParousPregIntervalToPowCalc1 * inputs.ParousPregIntervalToPowCalc1;
        Console.WriteLine($"Prior mean after ParousNoPeInverseOfPregInterval: {priorMean}");
        Console.WriteLine($"Input ParousPregIntervalToPowCalc1 {inputs.ParousPregIntervalToPowCalc1}");
        Console.WriteLine($"Coeff ParousPregIntervalToPowCalc1 {coeffs.ParousPregIntervalToPowCalc1}");
        Console.WriteLine($"Coeffs ParousPregIntervalToPowCalc1 calc: {coeffs.ParousPregIntervalToPowCalc1 * inputs.ParousPregIntervalToPowCalc1}");

        priorMean += coeffs.ParousPregIntervalToPowCalc2 * inputs.ParousPregIntervalToPowCalc2;
        Console.WriteLine($"Prior mean after parous ParousNoPeInverseSqRootOfPregInterval: {priorMean}");
        Console.WriteLine($"Input ParousPregIntervalToPowCalc2 {inputs.ParousPregIntervalToPowCalc2}");
        Console.WriteLine($"Coeff ParousPregIntervalToPowCalc2 {coeffs.ParousPregIntervalToPowCalc2}");
        Console.WriteLine($"Coeffs ParousPregIntervalToPowCalc2 calc: {coeffs.ParousPregIntervalToPowCalc2 * inputs.ParousPregIntervalToPowCalc2}");

        priorMean += coeffs.ParousNoPELastGestAge * inputs.ParousLastGestAgeWeeks;
        Console.WriteLine($"Prior mean after ParousNoPELastGestAge: {priorMean}");
        Console.WriteLine($"Coeffs ParousNoPELastGestAge calc: {coeffs.ParousNoPELastGestAge * inputs.ParousLastGestAgeWeeks}");

        priorMean -= coeffs.TwinFactor;
        // Console.WriteLine($"Prior mean after fetuses: {priorMean}");
        // Console.WriteLine($"Coeffs fetuses calc: {coeffs.TwinFactor}");

        Console.WriteLine($"FINAL priorMean: {priorMean}");
        Console.WriteLine("******");

        return priorMean;
    }
}