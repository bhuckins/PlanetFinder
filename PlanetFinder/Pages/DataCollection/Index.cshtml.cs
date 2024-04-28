using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanetFinder.AppCode.DataCollector;

namespace PlanetFinder.Pages.DataCollection
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            SwapiDataCollector dataCollector = new SwapiDataCollector();
            var results = dataCollector.GetPlanets();
        }
    }
}
