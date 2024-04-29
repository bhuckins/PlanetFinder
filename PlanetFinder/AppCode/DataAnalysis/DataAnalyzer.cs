using PlanetFinder.AppCode.DataObjects;
using PlanetFinder.AppCode.DataStore;

namespace PlanetFinder.AppCode.DataAnalysis
{
    public abstract class DataAnalyzer
    {
        public abstract IList<IPlanetGroup> GetPlanetGroups(IList<IPlanet> planets);
    }
}
