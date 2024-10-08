using static PeRiskCalc.Api.RiskCalculator;
using static PeRiskCalc.Api.PriorCalculator;
using Microsoft.AspNetCore.Mvc;
using PeRiskCalc.Api;

const double TARGET_GEST_AGE = 37;
const double PRIOR_SD_SINGLETON = 6.8833;
const double PRIOR_SD_TWINS = 4.6019;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(
    options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );

app.MapPost("/calculate-risk", ([FromBody] RiskCalculationRequest request) =>
{
    var priorCoeffs = PriorCoeffs.FromForm(request.PriorInputs);

    double priorMean = CalcPriorMean(priorCoeffs, request.PriorInputs);
    // Define the truncation limits for the log10 MoM values for each marker
    double[] lowerLimits = { -0.1224076, -0.4216152, -0.5655099 }; // MAP, UtA-PI, and PLGF respectively
    double[] upperLimits = { 0.12240759, 0.42161519, 0.56550992 }; // MAP, UtA-PI, and PLGF respectively

    double[,] sigma =
    {
        { 0.00141396, -0.0002726, -0.0001907 },
        { -0.0002726, 0.01630906, -0.0034539 },
        { -0.0001907, -0.0034539, 0.03147225 }
    };
    double[] momVector =
{
    Math.Max(lowerLimits[0], Math.Min(upperLimits[0], Math.Log10(request.PosteriorInputs.MapMoM))),
    Math.Max(lowerLimits[1], Math.Min(upperLimits[1], Math.Log10(request.PosteriorInputs.UtaPiMoM))),
    Math.Max(lowerLimits[2], Math.Min(upperLimits[2], Math.Log10(request.PosteriorInputs.PlgfMoM)))
};

    var mapAvailable = true;
    var utaPiAvailable = true;
    var plgfAvailable = true;
    if (request.PosteriorInputs.MapMoM <= 0)
    {
        mapAvailable = false;
    }
    if (request.PosteriorInputs.UtaPiMoM <= 0)
    {
        utaPiAvailable = false;
    }
    if (request.PosteriorInputs.PlgfMoM <= 0)
    {
        plgfAvailable = false;
    }

    var (riskPrior, riskPost) = Calculate(gestAge: Math.Max(request.PriorInputs.GestAgeWeeks, 24), targetGestAge: TARGET_GEST_AGE, priorMean, priorSD: request.PriorInputs.Fetuses == Fetuses.Singleton ? PRIOR_SD_SINGLETON : PRIOR_SD_TWINS, momVector, sigma, mapAvailable, utaPiAvailable, plgfAvailable);
    return Results.Ok(new { riskPrior, riskPost });
});

app.Run();
