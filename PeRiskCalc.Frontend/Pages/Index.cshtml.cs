using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PeRiskCalc.Frontend.Pages;

public class IndexModel : PageModel
{

    private readonly IConfiguration _configuration;
    public string ApiBaseUrl { get; private set; }

    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnGet()
    {
        // Retrieve the API base URL from configuration
        ApiBaseUrl = _configuration["ApiBaseUrl"]!;
    }
}
