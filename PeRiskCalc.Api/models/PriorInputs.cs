using System.Text.Json.Serialization;

namespace PeRiskCalc.Api;

public class PriorInputs
{
    // initialise truncation limits
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

    // declare / initialise inputs, applying truncation when necessary
    private double parousInverseOfPregInterval;
    private double parousInverseSqRootOfPregInterval;
    private double parousLastGestAgeCalc;
    private double parousLastGestAge;
    private double parousPregInterval;
    private int weight;
    private int age;
    private double height;

    [JsonConverter(typeof(FetusesJsonConverter))]
    public Fetuses Fetuses { get; set; }

    public bool IVF { get; set; }

    public int Age
    {
        get => age;
        set
        {
            value = Math.Max(AgeLowerLimit, Math.Min(value, AgeUpperLimit));
            age = value > 35 ? value - 35 : 0;
        }
    }

    public double Height
    {
        get => height;
        set
        {
            value = Math.Max(HeightLowerLimit, Math.Min(value, HeightUpperLimit));
            height = value - 164;
        }
    }

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

    public double ParousLastGestAgeCalc
    {
        get => parousLastGestAgeCalc; set
        {
            parousLastGestAgeCalc = Math.Pow(ParousLastGestAge - 24, 2);
        }
    }

    public bool ParousWithPE { get; set; }

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