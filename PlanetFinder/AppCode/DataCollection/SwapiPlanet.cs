using Microsoft.Extensions.Diagnostics.HealthChecks;
using PlanetFinder.AppCode.DataObjects;

namespace PlanetFinder.AppCode.DataCollection
{
    public class SwapiPlanet : IPlanet
    {
        public string Name { get; set; }

        // The SWAPI will return "unknown" for fields instead of null
        // We want to treat these as null, so translate those fields here
        public string ClimateAPI { get; set; }
        public string Climate
        {
            get
            {
                string climate;

                if (string.Compare(ClimateAPI, "unknown", true) == 0)
                    climate = null;
                else
                    climate = ClimateAPI;

                return climate;
            }
            // Shouldn't be anything trying to set the Climate of a SwapiPlanet - throw exception if they try
            set { throw new NotImplementedException(); }
        }

        public string GravityAPI { get; set; }
        public string Gravity
        {
            get
            {
                string gravity;

                if (string.Compare(GravityAPI, "unknown", true) == 0)
                    gravity = null;
                else
                    gravity = GravityAPI;

                return gravity;
            }
            set { throw new NotImplementedException(); }
        }

        
        // Want to parse the string population from the API into an integer
        public string PopulationAPI { get; set; }
        public int? Population
        {
            get
            {
                int? population;

                if (string.IsNullOrWhiteSpace(PopulationAPI) || string.Compare(PopulationAPI, "unknown", true) == 0)
                    population = null;
                else
                {
                    int temp;

                    if (!int.TryParse(PopulationAPI, out temp))
                        population = null;
                    else
                        population = temp;
                }

                return population;
            }
            
            set { throw new NotImplementedException(); }
        }

        // Want to to convert SurfaceWater to a decimal from a percent (Ex. .05 instead of 5 %)
        public string SurfaceWaterAPI { get; set; }
        public decimal? SurfaceWater
        {
            get
            {
                decimal? surfaceWater;

                if (string.IsNullOrWhiteSpace(SurfaceWaterAPI) || string.Compare(SurfaceWaterAPI, "unknown", true) == 0)
                    surfaceWater = null;
                else
                {
                    decimal temp;

                    if (!decimal.TryParse(SurfaceWaterAPI, out temp))
                        surfaceWater = null;
                    else
                        surfaceWater = temp / 100; // Convert from percent to decimal
                }

                return surfaceWater;
            }
            set { throw new NotImplementedException(); }
        }

        public string TerrainAPI { get; set; }
        public string Terrain
        {
            get
            {
                string terrain;

                if (string.Compare(TerrainAPI, "unknown", true) == 0)
                    terrain = null;
                else
                    terrain = TerrainAPI;

                return terrain;
            }
            set { throw new NotImplementedException(); }
        }
    }
}
