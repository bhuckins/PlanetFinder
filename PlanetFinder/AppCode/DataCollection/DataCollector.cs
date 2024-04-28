using PlanetFinder.AppCode.DataObjects;

namespace PlanetFinder.AppCode.DataCollection
{
    public abstract class DataCollector
    {
        public abstract List<IPlanet> GetPlanets();
    }
}
