using PlanetFinder.AppCode.DataObjects;
using PlanetFinder.AppCode.DataStore;
using System;

namespace PlanetFinder.AppCode.DataAnalysis
{
    public class PlanetGroupAnalyzer : DataAnalyzer
    {
        public override IList<IPlanetGroup> GetPlanetGroups(IList<IPlanet> planets)
        {
            List<IPlanetGroup> planetGroups = new List<IPlanetGroup>();

            // Planets shouldn't be null, but check just in case
            // If there are no planets to analyze then we're done
            if (planets == null || planets.Count == 0)
                return planetGroups;

            // The Planet data will have multiple climate and terrain attributes separated by commas
            // Split these out into all of the combinations, and count how many planets are in every combination
            // Store this in a dictionary for quicker lookups while processing

            Dictionary<(string Climate, string Terrain), int> planetGroupLookup = new Dictionary<(string, string), int>();

            foreach (IPlanet planet in planets)
            {
                List<string> climates = new List<string>();
                List<string> terrains = new List<string>();

                // Want to add a null value if the climate or terrain is null
                if (planet.Climate != null)
                    climates = planet.Climate.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList();
                else
                    climates.Add(null);

                if (planet.Terrain != null)
                    terrains = planet.Terrain.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList();
                else
                    terrains.Add(null);

                foreach (string climate in climates)
                {
                    foreach (string terrain in terrains)
                    {
                        var lookupKey = (climate, terrain);

                        if (!planetGroupLookup.ContainsKey(lookupKey))
                            planetGroupLookup.Add(lookupKey, 0);

                        planetGroupLookup[lookupKey]++;
                    }
                }
            }

            planetGroups = planetGroupLookup
                .Select(kvp => (IPlanetGroup)new PlanetGroup()
                {
                    Climate = kvp.Key.Climate,
                    Terrain = kvp.Key.Terrain,
                    PlanetCount = kvp.Value
                })
                .ToList();

            return planetGroups;
        }
    }
}
