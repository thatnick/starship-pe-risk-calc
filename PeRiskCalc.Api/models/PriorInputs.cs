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
    private double parousLastGestAgeWeeks;
    private double parousPregInterval;
    private int weight;
    private int age;
    private double height;

    [JsonConverter(typeof(FetusesJsonConverter))]
    public Fetuses Fetuses { get; set; }

    public double GestAgeWeeks { get; set; }

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

    public double ParousPregInterval
    {
        get => parousPregInterval; set
        {
            parousPregInterval = Math.Max(PregnancyIntervalLowerLimit, Math.Min(value, PregnancyIntervalUpperLimit));
        }
    }

    public double ParousLastGestAgeWeeks
    {
        get => parousLastGestAgeWeeks;
        set
        {
            value = Math.Max(LastGestAgeLowerLimit, Math.Min(value, LastGestAgeUpperLimit));
            parousLastGestAgeWeeks = Math.Pow(value - 24, 2);
        }
    }

    public bool ParousWithPE { get; set; }

    public double ParousPregIntervalToPowCalc1 => Math.Pow(ParousPregInterval, -1);

    public double ParousPregIntervalToPowCalc2 => Math.Pow(ParousPregInterval, -0.5);
}