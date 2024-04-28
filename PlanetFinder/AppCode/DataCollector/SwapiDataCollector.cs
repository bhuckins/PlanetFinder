using Newtonsoft.Json;
using PlanetFinder.AppCode.DataObjects;

namespace PlanetFinder.AppCode.DataCollector
{
    public class SwapiDataCollector : DataCollector
    {
        public override List<IPlanet> GetPlanets()
        {
            // There is no SWAPI function to get the data for all plants, so each one must be extracted individually
            // We can get the total number of planets from paging through the results at https://swapi.tech/api/planets
            // However, we'd have to page through the results, which uses up our limited number of API calls before getting throttled
            // So, just hardcode the number of planets here - could update to determine this dynamically if API throttling was not a concern
            const int MaxPlanetIndex = 10; // 60

            const string PlanetUrlFormatString = @"https://www.swapi.tech/api/planets/{0}";

            HttpClient client = new HttpClient();

            List<IPlanet> planets = new List<IPlanet>();

            for (int planetIndex = 1; planetIndex <= MaxPlanetIndex; planetIndex++)
            {
                string planetURL = string.Format(PlanetUrlFormatString, planetIndex);

                string jsonResponse = client.GetStringAsync(planetURL).Result;

                // Should log the error instead of continuing, but this is unlikely to occur
                // Even a bad request will still return a HTTP error code message
                if (string.IsNullOrWhiteSpace(jsonResponse))
                    continue;

                var planetResult = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                // Should log this error too, if the result we get is not JSON
                // Just continue for now
                if (planetResult == null)
                    continue;

                SwapiPlanet planet = new SwapiPlanet()
                {
                    Name = planetResult.result.properties.name,
                    Climate = planetResult.result.properties.climate,
                    Gravity = planetResult.result.properties.gravity,
                    PopulationAPI = planetResult.result.properties.population,
                    SurfaceWaterAPI = planetResult.result.properties.surface_water,
                    Terrain = planetResult.result.properties.terrain
                };

                planets.Add(planet);
            }

            return planets;
        }
    }
}
