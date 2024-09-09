using System.Text.Json.Serialization;

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

    private Fetuses Fetuses { get; set; }
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
            weight = ChronicHypert ? value : 0;
        }
    }

    [JsonConverter(typeof(EthnicityJsonConverter))]
    public required Ethnicity Ethnicity { get; set; }
    public bool MotherPE { get; set; }
    public bool ChronicHypert { get; set; }

    public bool Diabetes { get; set; }
    public bool SleOrAps { get; set; }

    public bool Parous { get; set; }
    
    public bool ParousWithPE { get; set; }

    private double ParousLastGestAge { get => parousLastGestAge; set {
        parousLastGestAge = Math.Max(LastGestAgeLowerLimit, Math.Min(value, LastGestAgeUpperLimit));
    } }

    private double ParousLastGestAgeCalc { get => parousLastGestAgeCalc; set {
        parousLastGestAgeCalc = Math.Pow(ParousLastGestAge - 24, 2);
    }}

    public double ParousPregInterval { get => parousPregInterval; set {
        parousPregInterval = Math.Max(PregnancyIntervalLowerLimit, Math.Min(value, PregnancyIntervalUpperLimit));
    } }

    public double ParousInverseOfPregInterval { get => parousInverseOfPregInterval; set {
        parousInverseOfPregInterval = 1 / value;
    } }
    public double ParousInverseSqRootOfPregInterval { get => parousInverseSqRootOfPregInterval; set {
        parousInverseSqRootOfPregInterval = 1 / Math.Sqrt(value);
    } }
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
    private double ethnicity;

    public double Constant { get; set; } = 54.3637;
    public double IVF { get; set; } = -1.6327;
    public double Age { get; set; } = -0.206886;
    public double Height { get; set; } = 0.11711;
    public double Weight { get; set; }
    public double Ethnicity { get => ethnicity; set {
        
    } }
    public double MotherPe { get; set; }
    public double ChronicHypert { get; set; } = -7.2897;
    public double Diabetes { get; set; }
    public double SleOrAps { get; set; } = -3.0519;

    public double ParousWithPE { get; set; }
    public double ParousWithPELastGestAge { get; set; }
    public double ParousNoPE { get; set; }
    public double ParousNoPEIntercept { get; set; }
    public double ParousNoPEIntervalOne { get; set; }
    public double ParousNoPEIntervalTwo { get; set; }
    public double ParousNoPELastGestAge { get; set; }

    public static PriorCoeffs FromForm(PriorInputs priorInputs)
    {
        var coeffs = new PriorCoeffs();

        // Ethnicity Logic
        coeffs.Ethnicity = priorInputs.Ethnicity switch
        {
            global::Ethnicity.SouthAsian => -1.129,
            global::Ethnicity.Black => -2.6786,
            _ => 0
        };

        if (priorInputs.Parous)
        {
            if (priorInputs.ParousWithPE) {
                coeffs.ParousWithPE = -8.1667;

            }
            

        }

        // Weight Logic based on Chronic Hypertension
        coeffs.Weight = !priorInputs.ChronicHypert ? -0.0694096 : 0;

        // Family History of PE based on Chronic Hypertension
        coeffs.MotherPe = !priorInputs.ChronicHypert ? -1.7154 : 0;

        // Diabetes Logic based on Chronic Hypertension
        coeffs.Diabetes = !priorInputs.ChronicHypert ? -3.3899 : 0;

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