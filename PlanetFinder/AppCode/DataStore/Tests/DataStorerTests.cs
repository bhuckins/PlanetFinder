using PlanetFinder.AppCode.DataCollection.Tests;
using PlanetFinder.AppCode.DataObjects;
using System.Diagnostics;

namespace PlanetFinder.AppCode.DataStore.Tests
{
    public class DataStorerTests
    {
        public bool RunTests()
        {
            MockDataCollector dataCollector = new MockDataCollector();

            List<IPlanet> planets = dataCollector.GetPlanets();

            AzureDataStorer dataStorer = new AzureDataStorer();

            dataStorer.DeleteAllPlanets();

            Debug.Assert(dataStorer.GetAllPlanets().Count == 0);

            dataStorer.SavePlanetsToStore(planets);

            Debug.Assert(dataStorer.GetAllPlanets().Count == 3);

            Debug.Assert(dataStorer.GetAllPlanets().Any(planet => planet.Name == "Earth"));
            Debug.Assert(dataStorer.GetAllPlanets().FirstOrDefault(planet => planet.Name == "Earth").Gravity == "1");

            Debug.Assert(dataStorer.GetAllPlanets().Any(planet => planet.Name == "Venus"));
            Debug.Assert(dataStorer.GetAllPlanets().Any(planet => planet.Name == "Mars"));

            return true;
        }
    }
}
