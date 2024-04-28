using PlanetFinder.AppCode.DataCollection.Tests;
using PlanetFinder.AppCode.DataStore.Tests;
using System.Diagnostics;

namespace PlanetFinder.AppCode.Gateway.Tests
{
    public class DataCollectionGatewayTests
    {
        public bool RunTests()
        {
            MockDataCollector dataCollector = new MockDataCollector();
            MockDataStorer dataStorer = new MockDataStorer();

            DataCollectionGateway gateway = new DataCollectionGateway(dataCollector, dataStorer);

            bool success;

            gateway.CollectAndStore(true, out success);

            Debug.Assert(success);

            Debug.Assert(dataCollector.GetPlanets().Count == dataStorer.GetAllPlanets().Count);

            return true;
        }
    }
}
