using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanetFinder.AppCode.DataAnalysis;
using PlanetFinder.AppCode.DataObjects;
using PlanetFinder.AppCode.DataStore;

namespace PlanetFinder.Pages.PlanetGroups
{
    public class IndexModel : PageModel
    {
        public IList<DataStorePlanetGroup> PlanetGroups { get; set; }

        public void OnGet()
        {
            AzureDataStorer dataStorer = new AzureDataStorer();

            PlanetGroups = dataStorer.GetAllPlanetGroups()
                .OrderByDescending(group => group.PlanetCount)
                .ThenBy(group => group.Climate)
                .ThenBy(group => group.Terrain)
                .ToList();
        }
    }
}
