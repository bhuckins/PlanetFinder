using PlanetFinder.AppCode.DataObjects;

namespace PlanetFinder.AppCode.DataStore
{
    public class AzureDataStore : DataStore
    {
        protected const string ConnectionString = @"";
        public override bool SaveToStore(IList<IPlanet> planets)
        {
            throw new NotImplementedException();
        }
        public override IList<IPlanet> GetAllPlanets()
        {
            throw new NotImplementedException();
        }
    }
}
