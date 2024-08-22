using static RiskCalculator;
using static PriorCalculator;
using Microsoft.AspNetCore.Mvc;

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
    double[] momVector = 
    {
        request.MoMValues.MapMoM,
        request.MoMValues.UtaPiMoM,
        request.MoMValues.PlgfMoM
    };
    double risk = CalcPostRisk(currentGestAge: Math.Max(12, 24), targetGestAge: 37, priorMean: priorMean, priorSD: 6.8833, MoMs: momVector, sigma: sigma);

    return Results.Ok(new { Risk = risk });
});

app.Run();
