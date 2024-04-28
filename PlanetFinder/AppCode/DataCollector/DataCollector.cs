using PlanetFinder.AppCode.DataObjects;

namespace PlanetFinder.AppCode.DataCollector
{
    public abstract class DataCollector
    {
        public abstract List<IPlanet> GetPlanets();
    }
}
