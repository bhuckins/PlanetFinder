using PlanetFinder.AppCode.DataObjects;

namespace PlanetFinder.AppCode.DataCollection.Tests
{
    public class MockDataCollector : DataCollector
    {
        public override List<IPlanet> GetPlanets()
        {
            return new List<IPlanet>()
            {
                new MockPlanet() { Name = "Earth", Climate = "mixed", Gravity = "1", Population = 810000000, SurfaceWater = .71M, Terrain = "mixed" }, // Population is 8.1 Billion, which is too large for Int32. Thankfully no planets in the Star Wars Universe is that large
                new MockPlanet() { Name = "Venus", Climate = "hot", Gravity = ".91", Population = 6, SurfaceWater = 0M, Terrain = "arid" }, // 6 landers on Venus
                new MockPlanet() { Name = "Mars", Climate = "cold", Gravity =  ".38", Population = 9, SurfaceWater = .14M, Terrain = "rocky" }
            };
        }
    }
}
