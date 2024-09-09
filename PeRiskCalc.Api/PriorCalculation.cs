using Microsoft.Extensions.Logging;

public static class PriorCalculator
{
    public static double CalcPriorMean(PriorCoeffs coeffs, PriorInputs inputs)
    {
        double priorMean = coeffs.Constant;
        System.Console.WriteLine("******");
        System.Console.WriteLine($"STARING priorMean: {priorMean}");
        System.Console.WriteLine($"Coeffs constant: {coeffs.Constant}");

        priorMean += coeffs.Age * inputs.Age;
        System.Console.WriteLine($"Prior mean after age: {priorMean}");
        System.Console.WriteLine($"Coeffs age calc: {coeffs.Age * inputs.Age}");

        priorMean += coeffs.Height * inputs.Height;
        System.Console.WriteLine($"Prior mean after height: {priorMean}");
        System.Console.WriteLine($"Coeffs height  calc: {coeffs.Height * inputs.Height}");

        priorMean += coeffs.Ethnicity;
        System.Console.WriteLine($"Prior mean after ethn: {priorMean}");
        System.Console.WriteLine($"Coeffs ethnicity  calc: {coeffs.Ethnicity * 0}");

        priorMean += coeffs.ChronicHypert * 0;
        System.Console.WriteLine($"Prior mean after ch: {priorMean}");
        System.Console.WriteLine($"Coeffs ChronicHypert  calc: {coeffs.ChronicHypert * 0}");

        priorMean += coeffs.SleOrAps * 0;
        System.Console.WriteLine($"Prior mean after autoim: {priorMean}");
        System.Console.WriteLine($"Coeffs Autoimmune  calc: {coeffs.SleOrAps * 0}");

        priorMean += coeffs.IVF * 0;
        System.Console.WriteLine($"Prior mean after ivf: {priorMean}");
        System.Console.WriteLine($"Coeffs IVF  calc: {coeffs.IVF * 0}");

        priorMean += coeffs.ParousWithPE * 0;
        System.Console.WriteLine($"Prior mean after parWPe: {priorMean}");
        System.Console.WriteLine($"Coeffs ParousWithPE  calc: {coeffs.ParousWithPE * 0}");

        priorMean += coeffs.ParousWithPELastGestAge * 0;
        System.Console.WriteLine($"Prior mean after parWithPEGestAge {priorMean}");
        System.Console.WriteLine($"Coeffs ParousWithPEGestAge  calc: {coeffs.ParousWithPELastGestAge * 0}");

        priorMean += coeffs.ParousNoPEIntercept * 0;
        System.Console.WriteLine($"Prior mean after parnopeinter: {priorMean}");
        System.Console.WriteLine($"Coeffs ParousNoPEInterval  calc: {coeffs.ParousNoPEIntercept * 0}");

        priorMean += coeffs.ParousNoPEIntervalOne * 0;
        System.Console.WriteLine($"Prior mean after parous peintsquared: {priorMean}");
        System.Console.WriteLine($"Coeffs ParousNoPEIntervalSquared  calc: {coeffs.ParousNoPEIntervalOne * 0}");

        priorMean += coeffs.ParousNoPEIntervalTwo * 0;
        System.Console.WriteLine($"Prior mean after parous peintsquared: {priorMean}");
        System.Console.WriteLine($"Coeffs ParousNoPEIntervalSquared  calc: {coeffs.ParousNoPEIntervalTwo * 0}");

        priorMean += coeffs.ParousNoPELastGestAge * 0;
        System.Console.WriteLine($"Prior mean after parousnopegestage: {priorMean}");
        System.Console.WriteLine($"Coeffs ParousNoPEGestationalAge  calc: {coeffs.ParousNoPELastGestAge * 0}");

        priorMean += coeffs.Weight * inputs.Weight;

        System.Console.WriteLine($"Prior mean after weight: {priorMean}");
        System.Console.WriteLine($"Coeffs Weight  calc: {coeffs.Weight * inputs.Weight}");

        priorMean += coeffs.MotherPe * 0;
        System.Console.WriteLine($"Prior mean after fam hist pe: {priorMean}");
        System.Console.WriteLine($"Coeffs FamilyHistoryPE  calc: {coeffs.MotherPe * 0}");

        priorMean += coeffs.Diabetes * 0;
        System.Console.WriteLine($"Prior mean after diabetes: {priorMean}");
        System.Console.WriteLine($"Coeffs diabetes  calc: {coeffs.Diabetes * 0}");

        Console.WriteLine($"FINAL priorMean: {priorMean}");
        System.Console.WriteLine("******");

        return priorMean;
    }
}
