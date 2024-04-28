using PlanetFinder.AppCode.DataObjects;

namespace PlanetFinder.AppCode.DataStore
{
    public abstract class DataStore
    {
        public abstract bool SaveToStore(IList<IPlanet> planets);

        public abstract IList<IPlanet> GetAllPlanets();
    }
}
