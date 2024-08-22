using static RiskCalculator;
using static PriorCalculator;
using Microsoft.AspNetCore.Mvc;

const double currentGestAge = 12;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(
    options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );

app.MapPost("/calculate-risk", ([FromBody] RiskCalculationRequest request) =>
{
    var priorCoeffs = PriorCoeffs.FromForm(request.PriorForm);
    double[,] sigma =
    {
        { 0.00141396, -0.0002726, -0.0001907 },
        { -0.0002726, 0.01630906, -0.0034539 },
        { -0.0001907, -0.0034539, 0.03147225 }
    };

    double priorMean = CalcPriorMean(priorCoeffs, request.PriorForm);
    // Define the truncation limits for the log10 MoM values for each marker
    double[] lowerLimits = { -0.1224076, -0.4216152, -0.5655099 }; // Example limits for MAP, UtA-PI, and PLGF respectively
    double[] upperLimits = { 0.12240759, 0.42161519, 0.56550992 }; // Example limits for MAP, UtA-PI, and PLGF respectively

    // Convert MoM values to log10 and apply truncation
    double[] momVector =
    {
    Math.Max(lowerLimits[0], Math.Min(upperLimits[0], Math.Log10(request.MoMValues.MapMoM))),
    Math.Max(lowerLimits[1], Math.Min(upperLimits[1], Math.Log10(request.MoMValues.UtaPiMoM))),
    Math.Max(lowerLimits[2], Math.Min(upperLimits[2], Math.Log10(request.MoMValues.PlgfMoM)))
};

    var (riskPrior, riskPost) = Calculate(gestationalAge: Math.Max(currentGestAge, 24), targetGestationalAge: 37, priorMean: priorMean, priorSD: 6.8833, moMs: momVector, sigma: sigma);
    return Results.Ok(new { riskPrior, riskPost });
});

app.Run();
