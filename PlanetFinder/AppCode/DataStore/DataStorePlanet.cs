using PlanetFinder.AppCode.DataObjects;

namespace PlanetFinder.AppCode.DataStore
{
    public class DataStorePlanet : IPlanet
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Climate { get; set; }
        public string Gravity { get; set; }
        public int? Population { get; set; }
        public decimal? SurfaceWater { get; set; }
        public string Terrain { get; set; }
    }
}
