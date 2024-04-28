using PlanetFinder.AppCode.DataObjects;
using System.Diagnostics;

namespace PlanetFinder.AppCode.DataCollection.Tests
{
    public class DataCollectionTests
    {
        public bool RunTests()
        {
            SwapiDataCollector collector = new SwapiDataCollector();

            List<IPlanet> planets = collector.GetPlanets();

            Debug.Assert(planets != null);

            Debug.Assert(planets.Count == 60);

            return true;
        }
    }
}
