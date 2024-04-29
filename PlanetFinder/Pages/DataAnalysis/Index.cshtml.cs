using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanetFinder.AppCode.DataAnalysis;
using PlanetFinder.AppCode.DataStore;
using PlanetFinder.AppCode.DataStore.Tests;
using PlanetFinder.AppCode.Gateway;

namespace PlanetFinder.Pages.DataAnalysis
{
    public class IndexModel : PageModel
    {
        public string LogString { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostAnalyzeData()
        {
            AzureDataStorer dataStorer = new AzureDataStorer();
            PlanetGroupAnalyzer dataAnalyzer = new PlanetGroupAnalyzer();
            
            DataAnalysisGateway gateway = new DataAnalysisGateway(dataStorer, dataAnalyzer);

            bool success;
            List<string> logEntries = gateway.AnalyzeAndStoreResults(true, out success);

            LogString = string.Join(Environment.NewLine, logEntries);

            return Page();
        }
    }
}
