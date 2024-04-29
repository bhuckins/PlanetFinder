using System.ComponentModel;

namespace PlanetFinder.AppCode.DataObjects
{
    public interface IPlanetGroup
    {
        public string Climate { get; set; }

        public string Terrain { get; set; }

        [DisplayName("# Planets")]
        public int PlanetCount { get; set; }
    }
}
