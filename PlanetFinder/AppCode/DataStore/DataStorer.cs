using PlanetFinder.AppCode.DataObjects;

namespace PlanetFinder.AppCode.DataStore
{
    public abstract class DataStorer
    {
        public abstract bool SavePlanetsToStore(IList<IPlanet> planets);

        public abstract IList<DataStorePlanet> GetAllPlanets();

        public abstract bool DeleteAllPlanets();

        public abstract bool SavePlanetGroupsToStore(IList<IPlanetGroup> planetGroups);

        public abstract IList<DataStorePlanetGroup> GetAllPlanetGroups();

        public abstract bool DeleteAllPlanetGroups();

        public abstract DataStorePlanetGroup GetPlanetGroup(int ID);

        public abstract IList<DataStorePlanet> GetMatchingPlanets(string climate, string terrain);
    }
}
