using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanetFinder.AppCode.DataAnalysis;
using PlanetFinder.AppCode.DataStore;

namespace PlanetFinder.Pages.PlanetGroups
{
    public class DetailsModel : PageModel
    {
        public DataStorePlanetGroup PlanetGroup { get; set; }

        public IList<DataStorePlanet> Planets { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AzureDataStorer dataStore = new AzureDataStorer();

            PlanetGroup = dataStore.GetPlanetGroup((int)id);

            Planets = dataStore.GetMatchingPlanets(PlanetGroup.Climate, PlanetGroup.Terrain)
                .OrderBy(planet => planet.Name)
                .ToList();

            return Page();
        }
    }
}
