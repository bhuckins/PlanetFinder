using PlanetFinder.AppCode.DataObjects;

namespace PlanetFinder.AppCode.DataAnalysis
{
    public class PlanetGroup : IPlanetGroup
    {
        public string Terrain { get; set; }

        public string Climate { get; set; }

        public int PlanetCount { get; set; }
    }
}
