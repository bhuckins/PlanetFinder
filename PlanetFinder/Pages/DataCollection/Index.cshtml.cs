using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanetFinder.AppCode.DataCollection;
using PlanetFinder.AppCode.DataStore;
using PlanetFinder.AppCode.Gateway;

namespace PlanetFinder.Pages.DataCollection
{
    public class IndexModel : PageModel
    {
        public string LogString { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPostCollectData()
        {
            SwapiDataCollector dataCollector = new SwapiDataCollector();
            AzureDataStorer dataStorer = new AzureDataStorer();
            DataCollectionGateway gateway = new DataCollectionGateway(dataCollector, dataStorer);

            bool success;
            List<string> logEntries = gateway.CollectAndStore(true, out success);

            LogString = string.Join(Environment.NewLine, logEntries);

            return Page();
        }
    }
}
