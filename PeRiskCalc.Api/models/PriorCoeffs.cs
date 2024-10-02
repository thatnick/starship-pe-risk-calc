namespace PeRiskCalc.Api;

public class PriorCoeffs
{
    // initialise all coefficients to default values
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
    public double ParousPregIntervalToPowCalc1 { get; set; } = -4.1513765;
    public double ParousPregIntervalToPowCalc2 { get; set; } = 9.21473572;
    public double ParousNoPELastGestAge { get; set; } = 0.01549673;

    // change coeffs from their default based on inputs
    public static PriorCoeffs FromForm(PriorInputs priorInputs)
    {
        var coeffs = new PriorCoeffs();
        // twins
        if (priorInputs.Fetuses != Fetuses.Singleton)
        {
            if (priorInputs.Fetuses == Fetuses.TwinMonochorionic)
            {
                coeffs.TwinFactor = 15.768;
            }
            else if (priorInputs.Fetuses == Fetuses.TwinDicorionic)
            {
                coeffs.TwinFactor = 17.115;
            }
        }

        // ethnicity
        coeffs.Ethnicity = priorInputs.Ethnicity switch
        {
            PeRiskCalc.Api.Ethnicity.SouthAsian => -1.129,
            PeRiskCalc.Api.Ethnicity.Black => -2.6786,
            _ => 0
        };

        // parity
        if (priorInputs.Parous)
        {
            if (priorInputs.ParousWithPE)
            {
                coeffs.ParousNoPEIntercept = 0;
                coeffs.ParousPregIntervalToPowCalc1 = 0;
                coeffs.ParousPregIntervalToPowCalc2 = 0;
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
            coeffs.ParousNoPEIntercept = 0;
            coeffs.ParousPregIntervalToPowCalc1 = 0;
            coeffs.ParousPregIntervalToPowCalc2 = 0;
            coeffs.ParousNoPELastGestAge = 0;

            coeffs.ParousWithPE = 0;
            coeffs.ParousWithPELastGestAge = 0;
        }

        // sle and aps
        if (!priorInputs.SleOrAps)
        {
            coeffs.SleOrAps = 0;
        }

        // ivf
        if (!priorInputs.IVF)
        {
            coeffs.IVF = 0;
        }

        // chronic hypetension
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