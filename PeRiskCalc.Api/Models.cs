using System.Text.Json.Serialization;

namespace PeRiskCalc.Api;

public class PriorInputs
{
    private static readonly int AgeLowerLimit = 12;
    private static readonly int AgeUpperLimit = 55;
    private static readonly int WeightLowerLimit = 34;
    private static readonly int WeightUpperLimit = 190;
    private static readonly int HeightLowerLimit = 127;
    private static readonly int HeightUpperLimit = 198;
    private static readonly double PregnancyIntervalLowerLimit = 0.25;
    private static readonly double PregnancyIntervalUpperLimit = 15;
    private static readonly double LastGestAgeLowerLimit = 24;
    private static readonly double LastGestAgeUpperLimit = 42;

    [JsonConverter(typeof(FetusesJsonConverter))]
    public Fetuses Fetuses { get; set; }
    private double GestAge { get; set; }
    public bool IVF { get; set; }
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

    private int weight;
    private double parousInverseOfPregInterval;
    private double parousInverseSqRootOfPregInterval;
    private double parousLastGestAgeCalc;
    private double parousLastGestAge;
    private double parousPregInterval;

    public int Weight
    {
        get => weight;
        set
        {
            value = Math.Max(WeightLowerLimit, Math.Min(value, WeightUpperLimit));
            weight = value - 69;
        }
    }

    [JsonConverter(typeof(EthnicityJsonConverter))]
    public required Ethnicity Ethnicity { get; set; }
    public bool MotherPE { get; set; }
    public bool ChronicHypert { get; set; }

    public bool Diabetes { get; set; }
    public bool SleOrAps { get; set; }

    public bool Parous { get; set; }

    public double ParousLastGestAgeCalc
    {
        get => parousLastGestAgeCalc; set
        {
            parousLastGestAgeCalc = Math.Pow(ParousLastGestAge - 24, 2);
        }
    }

    public bool ParousWithPE { get; set; }

    private double ParousLastGestAge
    {
        get => parousLastGestAge; set
        {
            parousLastGestAge = Math.Max(LastGestAgeLowerLimit, Math.Min(value, LastGestAgeUpperLimit));
        }
    }

    private double ParousPregInterval
    {
        get => parousPregInterval; set
        {
            parousPregInterval = Math.Max(PregnancyIntervalLowerLimit, Math.Min(value, PregnancyIntervalUpperLimit));
        }
    }

    public double ParousNoPeInverseOfPregInterval
    {
        get => parousInverseOfPregInterval; set
        {
            parousInverseOfPregInterval = 1 / value;
        }
    }
    public double ParousNoPeInverseSqRootOfPregInterval
    {
        get => parousInverseSqRootOfPregInterval; set
        {
            parousInverseSqRootOfPregInterval = 1 / Math.Sqrt(value);
        }
    }
}

public class PosteriorInputs
{
    public double MapMoM { get; set; }
    public double UtaPiMoM { get; set; }
    public double PlgfMoM { get; set; }
    public double PappAMoM { get; set; }
}

public class PriorCoeffs
{
    public double Constant { get; set; } = 54.3637;
    public double TwinFactor { get; set; } = 0;
    public double IVF { get; set; } = -1.6327;
    public double Age { get; set; } = -0.206886;
    public double Height { get; set; } = 0.11711;
    public double Weight { get; set; } = -0.0694096;
    public double Ethnicity { get; set; } = 0;
    public double MotherPe { get; set; } = -1.7154;
    public double ChronicHypert { get; set; } = -7.2897;
    public double Diabetes { get; set; } = -3.3899;
    public double SleOrAps { get; set; } = -3.0519;
    public double ParousWithPE { get; set; } = -8.1667;
    public double ParousWithPELastGestAge { get; set; } = 0.0271988;
    public double ParousNoPEIntercept { get; set; } = -4.335;
    public double ParousNoPeInverseOfPregInterval { get; set; } = -4.1513765;
    public double ParousNoPeInverseSqRootOfPregInterval { get; set; } = 9.21473572;
    public double ParousNoPELastGestAge { get; set; } = 0.01549673;

    public static PriorCoeffs FromForm(PriorInputs priorInputs)
    {
        var coeffs = new PriorCoeffs();
        if (priorInputs.Fetuses != global::Fetuses.Singleton)
        {
            if (priorInputs.Fetuses == global::Fetuses.TwinMonochorionic)
            {
                coeffs.TwinFactor = 15.768;
            }
            else if (priorInputs.Fetuses == global::Fetuses.TwinDicorionic)
            {
                coeffs.TwinFactor = 17.115;
            }
        }

        // Ethnicity Logic
        coeffs.Ethnicity = priorInputs.Ethnicity switch
        {
            global::Ethnicity.SouthAsian => -1.129,
            global::Ethnicity.Black => -2.6786,
            _ => 0
        };

        if (priorInputs.Parous)
        {
            if (priorInputs.ParousWithPE)
            {
                coeffs.ParousNoPEIntercept = 0;
                coeffs.ParousNoPeInverseOfPregInterval = 0;
                coeffs.ParousNoPeInverseSqRootOfPregInterval = 0;
                coeffs.ParousNoPELastGestAge = 0;
            }
            else
            {
                coeffs.ParousWithPE = 0;
                coeffs.ParousWithPELastGestAge = 0;
            }
        }
        else
        {
            coeffs.ParousWithPE = 0;
            coeffs.ParousWithPELastGestAge = 0;
            coeffs.ParousNoPEIntercept = 0;
            coeffs.ParousNoPeInverseOfPregInterval = 0;
            coeffs.ParousNoPeInverseSqRootOfPregInterval = 0;
            coeffs.ParousNoPELastGestAge = 0;
        }

        if (!priorInputs.SleOrAps)
        {
            coeffs.SleOrAps = 0;
        }

        if (!priorInputs.IVF)
        {
            coeffs.IVF = 0;
        }

        if (priorInputs.ChronicHypert)
        {
            coeffs.Weight = 0;
            coeffs.MotherPe = 0;
            coeffs.Diabetes = 0;
        }
        else
        {
            coeffs.ChronicHypert = 0;
            if (!priorInputs.MotherPE)
            {
                coeffs.MotherPe = 0;
            }
            if (!priorInputs.Diabetes)
            {
                coeffs.Diabetes = 0;
            }
        }
        return coeffs;
    }
}

public enum Fetuses
{
    Singleton,
    TwinMonochorionic,
    TwinDicorionic
}

public enum Ethnicity
{
    SouthAsian,
    Black,
    Other
}