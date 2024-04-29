using PlanetFinder.AppCode.DataObjects;
using System.ComponentModel;

namespace PlanetFinder.AppCode.DataStore
{
    public class DataStorePlanetGroup : IPlanetGroup
    {
        public int ID { get; set; }

        public string Climate { get; set; }

        public string Terrain { get; set; }

        [DisplayName("# Planets")]
        public int PlanetCount { get; set; }
    }
}
