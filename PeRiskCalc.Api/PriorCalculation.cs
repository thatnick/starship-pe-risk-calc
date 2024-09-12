public static class PriorCalculator
{
    public static double CalcPriorMean(PriorCoeffs coeffs, PriorInputs inputs)
    {
        double priorMean = coeffs.Constant;
        System.Console.WriteLine("******");
        System.Console.WriteLine($"STARTING priorMean: {priorMean}");
        System.Console.WriteLine($"Coeffs constant: {coeffs.Constant}");

        priorMean += coeffs.Age * inputs.Age;
        System.Console.WriteLine($"Prior mean after age: {priorMean}");
        System.Console.WriteLine($"Coeffs age calc: {coeffs.Age * inputs.Age}");

        priorMean += coeffs.Height * inputs.Height;
        System.Console.WriteLine($"Prior mean after height: {priorMean}");
        System.Console.WriteLine($"Coeffs height  calc: {coeffs.Height * inputs.Height}");

        priorMean += coeffs.Ethnicity;
        System.Console.WriteLine($"Prior mean after Ethnicity: {priorMean}");
        System.Console.WriteLine($"Coeffs ethnicity  calc: {coeffs.Ethnicity}");

        priorMean += coeffs.ChronicHypert;
        System.Console.WriteLine($"Prior mean after ChronicHypert: {priorMean}");
        System.Console.WriteLine($"Coeffs ChronicHypert  calc: {coeffs.ChronicHypert}");

        priorMean += coeffs.SleOrAps; 
        System.Console.WriteLine($"Prior mean after SleOrAps: {priorMean}");
        System.Console.WriteLine($"Coeffs SleOrAps  calc: {coeffs.SleOrAps}");

        priorMean += coeffs.IVF;
        System.Console.WriteLine($"Prior mean after ivf: {priorMean}");
        System.Console.WriteLine($"Coeffs IVF  calc: {coeffs.IVF}");

        priorMean += coeffs.ParousWithPE;
        System.Console.WriteLine($"Prior mean after ParousWithPE: {priorMean}");
        System.Console.WriteLine($"Coeffs ParousWithPE calc: {coeffs.ParousWithPE}");

        priorMean += coeffs.ParousWithPELastGestAge * inputs.ParousLastGestAgeCalc;
        System.Console.WriteLine($"Prior mean after parWithPEGestAge {priorMean}");
        System.Console.WriteLine($"Coeffs ParousWithPEGestAge calc: {coeffs.ParousWithPELastGestAge * inputs.ParousLastGestAgeCalc}");

        priorMean += coeffs.ParousNoPEIntercept;
        System.Console.WriteLine($"Prior mean after parnopeinter: {priorMean}");
        System.Console.WriteLine($"Coeffs ParousNoPEInterval calc: {coeffs.ParousNoPEIntercept}");

        priorMean += coeffs.ParousNoPeInverseOfPregInterval * inputs.ParousNoPeInverseOfPregInterval;
        System.Console.WriteLine($"Prior mean after ParousNoPeInverseOfPregInterval: {priorMean}");
        System.Console.WriteLine($"Coeffs ParousNoPeInverseOfPregInterval calc: {coeffs.ParousNoPeInverseOfPregInterval * inputs.ParousNoPeInverseOfPregInterval}");

        priorMean += coeffs.ParousNoPeInverseSqRootOfPregInterval * inputs.ParousNoPeInverseSqRootOfPregInterval;
        System.Console.WriteLine($"Prior mean after parous ParousNoPeInverseSqRootOfPregInterval: {priorMean}");
        System.Console.WriteLine($"Coeffs ParousNoPeInverseSqRootOfPregInterval calc: {coeffs.ParousNoPeInverseSqRootOfPregInterval * inputs.ParousNoPeInverseSqRootOfPregInterval}");

        priorMean += coeffs.ParousNoPELastGestAge * inputs.ParousLastGestAgeCalc;
        System.Console.WriteLine($"Prior mean after ParousNoPELastGestAge: {priorMean}");
        System.Console.WriteLine($"Coeffs ParousNoPELastGestAge calc: {coeffs.ParousNoPELastGestAge * inputs.ParousLastGestAgeCalc}");

        priorMean += coeffs.Weight * inputs.Weight;
        System.Console.WriteLine($"Prior mean after weight: {priorMean}");
        System.Console.WriteLine($"Coeffs Weight calc: {coeffs.Weight * inputs.Weight}");

        priorMean += coeffs.MotherPe;
        System.Console.WriteLine($"Prior mean after mother pe: {priorMean}");
        System.Console.WriteLine($"Coeffs mother pe calc: {coeffs.MotherPe}");

        priorMean += coeffs.Diabetes;
        System.Console.WriteLine($"Prior mean after diabetes: {priorMean}");
        System.Console.WriteLine($"Coeffs diabetes calc: {coeffs.Diabetes}");

        priorMean += coeffs.TwinFactor;
        System.Console.WriteLine($"Prior mean after fetuses: {priorMean}");
        System.Console.WriteLine($"Coeffs fetuses calc: {coeffs.TwinFactor}");

        Console.WriteLine($"FINAL priorMean: {priorMean}");
        System.Console.WriteLine("******");

        return priorMean;
    }
}
