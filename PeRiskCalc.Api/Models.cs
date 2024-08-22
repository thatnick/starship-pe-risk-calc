public class PriorForm
{
    private static readonly int AgeLowerLimit = 12;
    private static readonly int AgeUpperLimit = 55;
    private static readonly int WeightLowerLimit = 34;
    private static readonly int WeightUpperLimit = 190;
    private static readonly int HeightLowerLimit = 127;
    private static readonly int HeightUpperLimit = 198;
    private static readonly double PregnancyIntervalLowerLimit = 0.25;
    private static readonly double PregnancyIntervalUpperLimit = 15;
    private static readonly double PrevGestAgeLowerLimit = 24;
    private static readonly double PrevGestAgeUpperLimit = 42;

    private int age;
    public int Age
    {
        get => age;
        set
        {
            value = Math.Max(AgeLowerLimit, Math.Min(value, AgeUpperLimit));
            age = value > 35 ? value - 35 : 0;

        }
    }
    public bool ChronicHypert { get; set; }
    private int weight;
    public int Weight
    {
        get => weight;
        set
        {
            weight = ChronicHypert ? value : 0;
        }
    }

    private double height;
    public double Height
    {
        get => height;
        set
        {
            value = Math.Max(HeightLowerLimit, Math.Min(value, HeightUpperLimit));
            height = value - 164;
        }
    }

    public required String Ethnicity { get; set; }
    public bool FamilyHistoryPE { get; set; }
    public bool IVF { get; set; }

    public bool Diabetes { get; set; }
    public bool Autoimmune { get; set; }

    private double pregnancyInterval;
    //public double PregnancyInterval
    //{
    //    get => pregnancyInterval;
    //    set => pregnancyInterval = Math.Max(PregnancyIntervalLowerLimit, Math.Min(value, PregnancyIntervalUpperLimit));
    //}
    public double PregnancyInterval { get => pregnancyInterval; set => pregnancyInterval = value; }
    private double prevGestAge;
    //public double PrevGestAge
    //{
    //    get => prevGestAge;
    //    set => prevGestAge = Math.Max(PrevGestAgeLowerLimit, Math.Min(value, PrevGestAgeUpperLimit));
    //}
    public double PrevGestAge { get => prevGestAge; set => prevGestAge = value; }
    public void LogFormToConsole()
    {
        Console.WriteLine("Prior Form Data:");
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine($"Weight: {Weight}");
        Console.WriteLine($"Height: {Height}");
        Console.WriteLine($"Ethnicity: {Ethnicity}");
        Console.WriteLine($"FamilyHistoryPE: {FamilyHistoryPE}");
        Console.WriteLine($"Conception: {IVF}");
        Console.WriteLine($"ChronicHypert: {ChronicHypert}");
        Console.WriteLine($"Diabetes: {Diabetes}");
        Console.WriteLine($"Autoimmune: {Autoimmune}");
        Console.WriteLine($"PregnancyInterval: {PregnancyInterval}");
        Console.WriteLine($"PrevGestAge: {PrevGestAge}");
    }
}


public class MoMValues
{
    public double MapMoM { get; set; }
    public double UtaPiMoM { get; set; }
    public double PlgfMoM { get; set; }
}

public class PriorCoeffs
{
    public double Constant { get; set; } = 54.3637;
    public double Age { get; set; } = -0.206886;
    public double Height { get; set; } = 0.11711;
    public double Ethnicity { get; set; }
    public double ChronicHypert { get; set; } = -7.2897;
    public double Autoimmune { get; set; } = -3.0519;
    public double IVF { get; set; } = -1.6327;
    public double ParousWithPE { get; set; }
    public double ParousWithPEGestAge { get; set; }
    public double ParousNoPE { get; set; }
    public double ParousNoPEInterval { get; set; }
    public double ParousNoPEIntervalSquared { get; set; }
    public double ParousNoPEGestationalAge { get; set; }
    public double Weight { get; set; }
    public double FamilyHistoryPE { get; set; }
    public double Diabetes { get; set; }

    public void LogCoeffsToConsole()
    {
        Console.WriteLine("Prior Coefficients:");
        Console.WriteLine($"Constant: {Constant}");
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine($"Height: {Height}");
        Console.WriteLine($"Ethnicity: {Ethnicity}");
        Console.WriteLine($"ChronicHypert: {ChronicHypert}");
        Console.WriteLine($"Autoimmune: {Autoimmune}");
        Console.WriteLine($"Conception: {IVF}");
        Console.WriteLine($"ParousWithPE: {ParousWithPE}");
        Console.WriteLine($"ParousWithPEGestAge: {ParousWithPEGestAge}");
        Console.WriteLine($"ParousNoPE: {ParousNoPE}");
        Console.WriteLine($"ParousNoPEInterval: {ParousNoPEInterval}");
        Console.WriteLine($"ParousNoPEIntervalSquared: {ParousNoPEIntervalSquared}");
        Console.WriteLine($"ParousNoPEGestationalAge: {ParousNoPEGestationalAge}");
        Console.WriteLine($"Weight: {Weight}");
        Console.WriteLine($"FamilyHistoryPE: {FamilyHistoryPE}");
        Console.WriteLine($"Diabetes: {Diabetes}");
    }

    public static PriorCoeffs FromForm(PriorForm priorForm)
    {
        var coeffs = new PriorCoeffs();

        // Ethnicity Logic
        coeffs.Ethnicity = priorForm.Ethnicity switch
        {
            "white" => 0,
            "black" => -2.6786,
            _ => -1.129,
        };

        //// Parity Logic for "parous-with-pe"
        //if (priorForm.PregnancyInterval > 0 && priorForm.PrevGestAge > 0)
        //{
        //    coeffs.ParousWithPE = priorForm.PregnancyInterval == 1 ? -8.1667 : 0;
        //    coeffs.ParousWithPEGestAge = priorForm.PregnancyInterval == 1
        //        ? 0.0271988 * Math.Pow(priorForm.PrevGestAge - 24, 2)
        //        : 0;
        //}

        //// Parity Logic for "parous-no-pe"
        //if (priorForm.PregnancyInterval > 0)
        //{
        //    coeffs.ParousNoPE = priorForm.PregnancyInterval > 1 ? -4.335 : 0;
        //    coeffs.ParousNoPEInterval = priorForm.PregnancyInterval > 1
        //        ? -4.1513765 * (priorForm.PregnancyInterval - 1)
        //        : 0;
        //    coeffs.ParousNoPEIntervalSquared = priorForm.PregnancyInterval > 1
        //        ? 9.21473572 * (priorForm.PregnancyInterval - 0.5)
        //        : 0;
        //    coeffs.ParousNoPEGestationalAge = priorForm.PregnancyInterval > 1
        //        ? 0.01549673 * Math.Pow(priorForm.PrevGestAge - 24, 2)
        //        : 0;
        //}

        // Weight Logic based on Chronic Hypertension
        coeffs.Weight = !priorForm.ChronicHypert ? -0.0694096 : 0;

        // Family History of PE based on Chronic Hypertension
        coeffs.FamilyHistoryPE = !priorForm.ChronicHypert ? -1.7154 : 0;

        // Diabetes Logic based on Chronic Hypertension
        coeffs.Diabetes = !priorForm.ChronicHypert ? -3.3899 : 0;

        priorForm.LogFormToConsole();
        coeffs.LogCoeffsToConsole();
        return coeffs;
    }
}

