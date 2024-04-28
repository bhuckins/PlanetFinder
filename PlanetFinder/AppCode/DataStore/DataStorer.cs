using PlanetFinder.AppCode.DataObjects;

namespace PlanetFinder.AppCode.DataStore
{
    public abstract class DataStorer
    {
        public abstract bool SaveToStore(IList<IPlanet> planets);

        public abstract IList<IPlanet> GetAllPlanets();

        public abstract bool DeleteAllPlanets();
    }
}
