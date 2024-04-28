using PlanetFinder.AppCode.DataObjects;

namespace PlanetFinder.AppCode.DataStore.Tests
{
    public class MockDataStorer : DataStorer
    {
        protected List<IPlanet> Planets { get; set; }

        public MockDataStorer()
        {
            Planets = new List<IPlanet>();
        }
        public override bool DeleteAllPlanets()
        {
            Planets.Clear();

            return true;
        }

        public override IList<IPlanet> GetAllPlanets()
        {
            return Planets;
        }

        public override bool SaveToStore(IList<IPlanet> planets)
        {
            Planets.AddRange(planets);

            return true;

        }
    }
}
