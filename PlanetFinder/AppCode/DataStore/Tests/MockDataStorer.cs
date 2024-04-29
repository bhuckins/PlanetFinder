using PlanetFinder.AppCode.DataObjects;

namespace PlanetFinder.AppCode.DataStore.Tests
{
    public class MockDataStorer : DataStorer
    {
        protected List<DataStorePlanet> Planets { get; set; }

        protected List<DataStorePlanetGroup> PlanetGroups { get; set; }

        public MockDataStorer()
        {
            Planets = new List<DataStorePlanet>();
            PlanetGroups = new List<DataStorePlanetGroup>();
        }

        public override bool SavePlanetsToStore(IList<IPlanet> planets)
        {
            foreach (IPlanet planet in planets)
                Planets.Add(new DataStorePlanet()
                {
                    ID = planet.GetHashCode(),
                    Name = planet.Name,
                    Climate = planet.Climate,
                    Gravity = planet.Gravity,
                    Population = planet.Population,
                    SurfaceWater = planet.SurfaceWater,
                    Terrain = planet.Terrain

                });

            return true;
        }

        public override IList<DataStorePlanet> GetAllPlanets()
        {
            return Planets;
        }

        public override bool DeleteAllPlanets()
        {
            Planets.Clear();

            return true;
        }

        public override bool SavePlanetGroupsToStore(IList<IPlanetGroup> planetGroups)
        {
            foreach (IPlanetGroup planetGroup in planetGroups)
                PlanetGroups.Add(new DataStorePlanetGroup()
                {
                    ID = planetGroup.GetHashCode(),
                    Climate = planetGroup.Climate,
                    Terrain = planetGroup.Terrain,
                    PlanetCount = planetGroup.PlanetCount
                });

            return true;
        }

        public override IList<DataStorePlanetGroup> GetAllPlanetGroups()
        {
            return PlanetGroups;
        }

        public override bool DeleteAllPlanetGroups()
        {
            PlanetGroups.Clear();

            return true;
        }

        public override DataStorePlanetGroup GetPlanetGroup(int ID)
        {
            return PlanetGroups.FirstOrDefault(planetGroup => planetGroup.ID == ID);
        }

        public override IList<DataStorePlanet> GetMatchingPlanets(string climate, string terrain)
        {
            return Planets
                .Where(planet => (string.IsNullOrWhiteSpace(climate) && string.IsNullOrWhiteSpace(planet.Climate)) || (!string.IsNullOrWhiteSpace(planet.Climate) && planet.Climate.Contains(climate)))
                .Where(planet => (string.IsNullOrWhiteSpace(terrain) && string.IsNullOrWhiteSpace(planet.Terrain)) || (!string.IsNullOrWhiteSpace(planet.Terrain) && planet.Terrain.Contains(terrain)))
                .ToList();
        }
    }
}
