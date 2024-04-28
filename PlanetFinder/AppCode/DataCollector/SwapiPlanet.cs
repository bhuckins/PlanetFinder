using PlanetFinder.AppCode.DataObjects;

namespace PlanetFinder.AppCode.DataCollector
{
    public class SwapiPlanet : IPlanet
    {
        public string Name { get; set; }
        public string Climate { get; set; }
        public string Gravity { get; set; }

        // The SWAPI will return "unknown" for planets without a known population
        // We want to be able to treat the population as an integer, so translate "unknown" to null
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
            // Shouldn't be anything trying to set the Population of a SwapiPlanet - throw exception if they try
            set { throw new NotImplementedException(); }
        }

        // Same for SurfaceWater
        // Additionally, want to to convert SurfaceWater to a decimal from a percent (Ex. .05 instead of 5 %)
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
        public string Terrain { get; set; }
    }
}
